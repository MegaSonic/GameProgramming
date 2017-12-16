using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Gun/Projectile", order = 1)]
public class ProjectileData : GunData {

    public GunType gunType = GunType.PROJECTILE;

    public List<ProjectileStruct> gunLevelData;

    [System.Serializable]
    public struct ProjectileStruct
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
    }
}
