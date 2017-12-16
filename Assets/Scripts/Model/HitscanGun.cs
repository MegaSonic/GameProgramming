using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HitscanGun : Gun {

    [SerializeField]
    protected Transform emitTransform;

    [SerializeField]
    protected DamageType damageType;

    [SerializeField]
    protected int minDamage;

    [SerializeField]
    protected int maxDamage;

    [SerializeField]
    protected GameObject hitscanLine;

    [SerializeField]
    protected float cooldown;

    [SerializeField]
    private HitscanData hitscanData;

    protected float cooldownTimer = 0f;

    public override void Shoot()
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0f;
            Ray ray = new Ray(emitTransform.position, emitTransform.forward);
            RaycastHit hit;
            Debug.DrawRay(emitTransform.position, emitTransform.forward, Color.red, 1f);

            GameObject newLine = Instantiate(hitscanLine);
            LineRenderer line = newLine.GetComponent<LineRenderer>();
            line.SetPosition(0, emitTransform.position);
            line.SetPosition(1, emitTransform.TransformPoint(emitTransform.localPosition + new Vector3(0f, 0f, 300f)));

            if (Physics.Raycast(ray, out hit))
            {
                IHealth damageable = hit.collider.gameObject.GetComponent<IHealth>();

                if (damageable != null)
                {
                    int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
                    
                    // Debug.Log(damage);
                    damageable.Damage(transform.root.gameObject, damageType, damage);

                    if (EventManager.OnDamagedSomething != null)
                    {
                        EventManager.OnDamagedSomething(transform.root.name, hit.collider.transform.root.name, damageType, damage);
                    }
                }
            }
        }
    }

    private void Awake()
    {
        EventManager.OnEnemyKilled += MakeWeaponOnKill;
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyKilled -= MakeWeaponOnKill;
    }

    public void MakeWeaponOnKill(string name, string source, Transform transform)
    {
        if (gameObject.activeSelf)
        {
            int newLevel = UnityEngine.Random.Range(1, hitscanData.gunLevelData.Count + 1);

            if (newLevel >= this.level)
            {
                GenerateWeapon(newLevel);
            }
        }
    }


    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        if (minDamage > maxDamage)
        {
            Debug.LogWarning("Min damage greater than max damage in gun " + transform.name);
        }
#endif

        GenerateWeapon(1);
    }

    private void Update()
    {
        if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            GenerateWeapon();
        }
#endif
    }

    

    public override void GenerateWeapon(int level)
    {
        if (level <= 0) level = 1;
        if (level > hitscanData.gunLevelData.Count) level = hitscanData.gunLevelData.Count;

        HitscanData.HitscanStruct data = hitscanData.gunLevelData[level - 1];
        this.level = level;
        damageType = data.possibleDamageTypes[UnityEngine.Random.Range(0, (int)data.possibleDamageTypes.Count)];

        minDamage = UnityEngine.Random.Range(data.damageMinimum, data.damageMaximum + 1);
        maxDamage = minDamage + UnityEngine.Random.Range(data.damageRangeMinimum, data.damageRangeMaximum + 1);

        cooldown = UnityEngine.Random.Range(data.minCooldown, data.maxCooldown);

        text.text = GetString();
    }

    public string GetString()
    {
        StringBuilder newString = new StringBuilder("");
        newString.AppendFormat("{0} \n", hitscanData.gunType.ToString());
        newString.AppendFormat("Level: {0} \n", level);
        newString.AppendFormat("Damage Type: {0} \nMin Damage: {1} \nMax Damage: {2} \nFire Rate: {3}", damageType.ToString(), minDamage, maxDamage, cooldown);
        return newString.ToString();
    }

    public override void GenerateWeapon()
    {
        GenerateWeapon(UnityEngine.Random.Range(1, hitscanData.gunLevelData.Count + 1));
    }
}
