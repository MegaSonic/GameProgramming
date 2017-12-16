using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitscanData", menuName = "Gun/Hitscan", order = 4)]
public class HitscanData : GunData {

    public GunType gunType = GunType.HITSCAN;

    public List<HitscanStruct> gunLevelData;

    [System.Serializable]
    public struct HitscanStruct
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
    }
}
