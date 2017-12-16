using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet {

    private float explosionRadius;
    private float coreRadius;
    private int splashDamage;

    private void OnTriggerEnter(Collider other)
    {

        Collider[] hits = Physics.OverlapSphere(this.transform.position, explosionRadius);
        foreach (Collider hit in hits)
        {
            if (Vector3.Distance(this.transform.position, hit.transform.position) < coreRadius)
            {
                IHealth damageable = hit.GetComponent<IHealth>();

                if (damageable != null)
                {
                    if (EventManager.OnDamagedSomething != null)
                    {
                        EventManager.OnDamagedSomething(transform.name, hit.gameObject.name, damageType, damage);
                    }
                    damageable.Damage(bulletSource, damageType, damage);
                    
                }
            }
            else
            {
                IHealth damageable = hit.GetComponent<IHealth>();

                if (damageable != null)
                {
                    if (EventManager.OnDamagedSomething != null)
                    {
                        EventManager.OnDamagedSomething(transform.name, hit.gameObject.name, damageType, splashDamage);
                    }
                    damageable.Damage(bulletSource, damageType, splashDamage);
                }
            }
        }

        Destroy(this.gameObject);
    }

    public void Shoot(Vector3 direction, GameObject source, DamageType type, int bulletDamage, int splashDamage, float bulletSpeed, float explosionRadius, float coreRadius, float headshotMultiplier)
    {
        bulletSource = source;
        damageType = type;
        speed = bulletSpeed;
        this.headshotMultiplier = headshotMultiplier;
        rigid.velocity = direction.normalized * speed;
        damage = bulletDamage;
        this.splashDamage = splashDamage;
        this.explosionRadius = explosionRadius;
        this.coreRadius = coreRadius;
    }
}
