﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    protected float timeTilDeath;

    protected DamageType damageType;

    protected float speed;

    protected int damage;

    protected float headshotMultiplier;

    protected Rigidbody rigid;
    protected float deathTimer;
    protected bool destroyed;
    protected GameObject bulletSource;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        deathTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        deathTimer += Time.deltaTime;
        if (deathTimer >= timeTilDeath)
        {
            Destroy(this.gameObject);
        }
    }


    public void SetTimeTilDeath(int value)
    {
        timeTilDeath = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        IHealth damageable = other.gameObject.GetComponent<IHealth>();

        if (damageable != null)
        {
            if (EventManager.OnDamagedSomething != null)
            {
                EventManager.OnDamagedSomething(transform.name, other.transform.name, damageType, damage);
            }

            damageable.Damage(bulletSource, damageType, damage);
            Destroy(this.gameObject);
        }
    }

    public void Shoot(Vector3 direction, GameObject source, DamageType type, int bulletDamage, float bulletSpeed, float headshotMultiplier)
    {
        bulletSource = source;
        damageType = type;
        speed = bulletSpeed;
        this.headshotMultiplier = headshotMultiplier;
        rigid.velocity = direction.normalized * speed;
        damage = bulletDamage;
    }
}
