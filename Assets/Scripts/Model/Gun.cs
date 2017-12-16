using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour, IShootable {

    [SerializeField]
    protected int level;

    [SerializeField]
    protected Text text;

    public abstract void Shoot();
    public abstract void GenerateWeapon(int level);
    public abstract void GenerateWeapon();
}


public enum GunType
{
    PROJECTILE = 0,
    MISSILE = 1,
    SHOTGUN = 2,
    HITSCAN = 3,
    SMG = 4
}
