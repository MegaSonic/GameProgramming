using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ProjectileGun : Gun
{

    [SerializeField]
    private Transform emitPoint;

    [SerializeField]
    private Transform emitDirection;

    [SerializeField]
    private GameObject bulletObject;

    [SerializeField]
    private DamageType damageType;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float headshotMultiplier;

    [SerializeField]
    private int minDamage;

    [SerializeField]
    private int maxDamage;

    private void Start()
    {
#if UNITY_EDITOR
        if (bulletObject == null)
        {
            Debug.LogWarning("Warning! Gun has no bullet projectile prefab!");
        }
#endif
    }

    public override void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletObject, emitPoint.position, Quaternion.identity);
        bulletInstance.SetActive(true);
        int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
        bulletInstance.GetComponent<Bullet>().Shoot(emitPoint.forward, transform.root.gameObject, damageType, damage, bulletSpeed, headshotMultiplier);
        bulletInstance.gameObject.tag = this.transform.root.tag;
    }
}
