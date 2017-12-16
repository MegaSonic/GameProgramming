using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ProjectileGun : Gun
{

    [SerializeField]
    protected Transform emitPoint;

    [SerializeField]
    protected Transform emitDirection;

    [SerializeField]
    protected GameObject bulletObject;

    [SerializeField]
    protected DamageType damageType;

    [SerializeField]
    protected float bulletSpeed;

    [SerializeField]
    protected float headshotMultiplier;

    [SerializeField]
    protected int minDamage;

    [SerializeField]
    protected int maxDamage;

    [SerializeField]
    protected float cooldown;

    [SerializeField]
    private ProjectileData projectileData;

    protected float cooldownTimer = 0f;

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
            int newLevel = UnityEngine.Random.Range(1, projectileData.gunLevelData.Count + 1);

            if (newLevel >= this.level)
            {
                GenerateWeapon(newLevel);
            }
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (bulletObject == null)
        {
            Debug.LogWarning("Warning! Gun has no bullet projectile prefab!");
        }

#endif

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

    public override void Shoot()
    {
        if (cooldownTimer >= cooldown)
        {
            GameObject bulletInstance = Instantiate(bulletObject, emitPoint.position, Quaternion.identity);
            bulletInstance.SetActive(true);
            int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
            bulletInstance.GetComponent<Bullet>().Shoot(emitPoint.forward, transform.root.gameObject, damageType, damage, bulletSpeed, headshotMultiplier);
            bulletInstance.gameObject.tag = this.transform.root.tag;
            cooldownTimer = 0f;
        }
    }

    public string GetString()
    {
        StringBuilder newString = new StringBuilder("");
        newString.AppendFormat("{0} \n", projectileData.gunType.ToString());
        newString.AppendFormat("Level: {0} \n", level);
        newString.AppendFormat("Damage Type: {0} \nMin Damage: {1} \nMax Damage: {2} \nFire Rate: {3}", damageType.ToString(), minDamage, maxDamage, cooldown);
        return newString.ToString();
    }

    public override void GenerateWeapon(int level)
    {
        if (level <= 0) level = 1;
        if (level > projectileData.gunLevelData.Count) level = projectileData.gunLevelData.Count;

        ProjectileData.ProjectileStruct data = projectileData.gunLevelData[level - 1];
        bulletSpeed = UnityEngine.Random.Range(data.minBulletSpeed, data.maxBulletSpeed);
        this.level = level;
        damageType = data.possibleDamageTypes[UnityEngine.Random.Range(0, (int) data.possibleDamageTypes.Count)];

        headshotMultiplier = UnityEngine.Random.Range(data.minHeadshotMultiplier, data.maxHeadshotMultiplier);
        minDamage = UnityEngine.Random.Range(data.damageMinimum, data.damageMaximum);
        maxDamage = minDamage + UnityEngine.Random.Range(data.damageRangeMinimum, data.damageRangeMaximum);
        cooldown = UnityEngine.Random.Range(data.minCooldown, data.maxCooldown);

        text.text = GetString();
    }

    public override void GenerateWeapon()
    {
        GenerateWeapon(UnityEngine.Random.Range(1, projectileData.gunLevelData.Count + 1));
    }
}
