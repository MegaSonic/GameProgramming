using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MissileGun : ProjectileGun {

    [SerializeField]
    private int splashDamage;

    [SerializeField]
    private float coreRadius;

    [SerializeField]
    private float splashRadius;

    [SerializeField]
    private MissileData missileData;

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

    private void Start()
    {
        GenerateWeapon(1);
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
            int newLevel = UnityEngine.Random.Range(1, missileData.gunLevelData.Count + 1);

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
            GameObject bulletInstance = Instantiate(bulletObject, emitPoint.position, Quaternion.identity);
            bulletInstance.SetActive(true);
            int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
            bulletInstance.GetComponent<Missile>().Shoot(emitPoint.forward, transform.root.gameObject, damageType, damage, splashDamage, bulletSpeed, splashRadius, coreRadius, headshotMultiplier);
            bulletInstance.gameObject.tag = this.transform.root.tag;
        }
    }

    public override void GenerateWeapon(int level)
    {
        if (level <= 0) level = 1;
        if (level > missileData.gunLevelData.Count) level = missileData.gunLevelData.Count;

        MissileData.MissileStruct data = missileData.gunLevelData[level - 1];
        bulletSpeed = UnityEngine.Random.Range(data.minBulletSpeed, data.maxBulletSpeed);
        this.level = level;
        damageType = data.possibleDamageTypes[UnityEngine.Random.Range(0, (int)data.possibleDamageTypes.Count)];

        headshotMultiplier = UnityEngine.Random.Range(data.minHeadshotMultiplier, data.maxHeadshotMultiplier);
        minDamage = UnityEngine.Random.Range(data.damageMinimum, data.damageMaximum);
        maxDamage = minDamage + UnityEngine.Random.Range(data.damageRangeMinimum, data.damageRangeMaximum);
        cooldown = UnityEngine.Random.Range(data.minCooldown, data.maxCooldown);

        splashDamage = UnityEngine.Random.Range(data.splashDamageMin, data.splashDamageMax);
        coreRadius = UnityEngine.Random.Range(data.coreRadiusMin, data.coreRadiusMax);
        splashRadius = coreRadius + UnityEngine.Random.Range(data.splashRadiusMin, data.splashRadiusMax);

        text.text = GetString();
    }

    public new string GetString()
    {
        StringBuilder newString = new StringBuilder("");
        newString.AppendFormat("{0} \n", missileData.gunType.ToString());
        newString.AppendFormat("Level: {0} \n", level);
        newString.AppendFormat("Damage Type: {0} \nMin Damage: {1} \nMax Damage: {2} \nFire Rate: {3} \nSplash Damage: {4}", damageType.ToString(), minDamage, maxDamage, cooldown, splashDamage);
        return newString.ToString();
    }

    public override void GenerateWeapon()
    {
        GenerateWeapon(UnityEngine.Random.Range(1, missileData.gunLevelData.Count + 1));
    }
}
