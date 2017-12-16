using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Shotgun : HitscanGun {

    [SerializeField]
    protected float maxSpread;

    [SerializeField]
    protected int pellets;

    [SerializeField]
    protected Transform dummyPoint;

    [SerializeField]
    protected float dummyPointDistance;

    [SerializeField]
    private ShotgunData shotgunData;

    private void Start()
    {
        GenerateWeapon(1);
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            GenerateWeapon();
        }
#endif
    }

    private void Awake()
    {
        EventManager.OnEnemyKilled += MakeWeaponOnKill;
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyKilled -= MakeWeaponOnKill;
    }

    public new void MakeWeaponOnKill(string name, string source, Transform transform)
    {
        if (gameObject.activeSelf)
        {
            int newLevel = UnityEngine.Random.Range(1, shotgunData.gunLevelData.Count + 1);

            if (newLevel >= this.level)
            {
                GenerateWeapon(newLevel);

            }
        }
    }

    public override void Shoot()
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0f;

            for (int i = 0; i < pellets; i++)
            {
                Vector3 vector = GetUnitCircle(i);

                dummyPoint.localPosition = new Vector3(vector.x, vector.y, dummyPointDistance);

                Ray ray = new Ray(emitTransform.position, dummyPoint.position - emitTransform.position);

                RaycastHit hit;
                Debug.DrawRay(emitTransform.position, dummyPoint.position - emitTransform.position, Color.blue, 1f);
                GameObject newLine = Instantiate(hitscanLine);

                LineRenderer line = newLine.GetComponent<LineRenderer>();
                line.SetPosition(0, emitTransform.position);
                line.SetPosition(1, dummyPoint.position);

                if (Physics.Raycast(ray, out hit))
                {
                    IHealth damageable = hit.collider.gameObject.GetComponent<IHealth>();

                    if (damageable != null)
                    {
                        int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);

                        damageable.Damage(transform.root.gameObject, damageType, damage);

                        if (EventManager.OnDamagedSomething != null)
                        {
                            EventManager.OnDamagedSomething(transform.root.name, hit.collider.transform.root.name, damageType, damage);
                        }
                    }
                }
            }
        }
    }

    public Vector3 GetUnitCircle(int i)
    {
        float x = Mathf.Cos(Mathf.Deg2Rad * 360 / pellets * i);
        float y = Mathf.Sin(Mathf.Deg2Rad * 360 / pellets * i);
        

        Vector3 vector = new Vector3(x, y, 0f) * Random.Range(0, maxSpread);
        return vector;
    }

    public new string GetString()
    {
        StringBuilder newString = new StringBuilder("");
        newString.AppendFormat("{0} \n", shotgunData.gunType.ToString());
        newString.AppendFormat("Level: {0} \n", level);
        newString.AppendFormat("Damage Type: {0} \nMin Damage: {1} \nMax Damage: {2} \nFire Rate: {3} \nPellets: {4} \nSpread: {5}", damageType.ToString(), minDamage, maxDamage, cooldown, pellets, maxSpread);
        return newString.ToString();
    }


    public override void GenerateWeapon(int level)
    {
        if (level <= 0) level = 1;
        if (level > shotgunData.gunLevelData.Count) level = shotgunData.gunLevelData.Count;

        ShotgunData.ShotgunStruct data = shotgunData.gunLevelData[level - 1];
        this.level = level;
        damageType = data.possibleDamageTypes[UnityEngine.Random.Range(0, (int)data.possibleDamageTypes.Count)];

        minDamage = UnityEngine.Random.Range(data.damageMinimum, data.damageMaximum + 1);
        maxDamage = minDamage + UnityEngine.Random.Range(data.damageRangeMinimum, data.damageRangeMaximum + 1);

        maxSpread = Random.Range(data.minSpread, data.maxSpread);
        dummyPointDistance = Random.Range(data.rangeMin, data.rangeMax);
        pellets = Random.Range(data.minPellets, data.maxPellets + 1);

        cooldown = UnityEngine.Random.Range(data.minCooldown, data.maxCooldown);

        text.text = GetString();

    }

    public override void GenerateWeapon()
    {
        GenerateWeapon(UnityEngine.Random.Range(1, shotgunData.gunLevelData.Count + 1));
    }
}
