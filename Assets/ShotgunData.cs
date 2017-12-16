using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunData", menuName = "Gun/Shotgun", order = 2)]
public class ShotgunData : GunData {

    public GunType gunType = GunType.SHOTGUN;

    public List<ShotgunStruct> gunLevelData;

    [System.Serializable]
    public struct ShotgunStruct
    {
        public int level;
        public List<DamageType> possibleDamageTypes;

        public float minHeadshotMultiplier;
        public float maxHeadshotMultiplier;

        public int damageMinimum;
        public int damageMaximum;

        public int damageRangeMinimum;
        public int damageRangeMaximum;

        public float minCooldown;
        public float maxCooldown;

        public float minSpread;
        public float maxSpread;

        public int minPellets;
        public int maxPellets;

        public float rangeMin;
        public float rangeMax;
    }
}
