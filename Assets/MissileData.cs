using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissileData", menuName = "Gun/Missile", order = 3)]
public class MissileData : GunData {

    public GunType gunType = GunType.MISSILE;

    public List<MissileStruct> gunLevelData;

    [System.Serializable]
    public struct MissileStruct
    {
        public int level;
        public List<DamageType> possibleDamageTypes;

        public float minBulletSpeed;
        public float maxBulletSpeed;

        public float minHeadshotMultiplier;
        public float maxHeadshotMultiplier;

        public int damageMinimum;
        public int damageMaximum;

        public int damageRangeMinimum;
        public int damageRangeMaximum;

        public float minCooldown;
        public float maxCooldown;

        public int splashDamageMin;
        public int splashDamageMax;

        public int coreRadiusMin;
        public int coreRadiusMax;

        public float splashRadiusMin;
        public float splashRadiusMax;
    }
}
