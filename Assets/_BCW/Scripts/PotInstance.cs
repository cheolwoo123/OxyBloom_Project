using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotInstance
{
    public PotData potData;
    public int level;

    public PotInstance(PotData data)
    {
        potData = data;
        level = 1;
    }

    
    public float GetGrowthBonus() => potData.growthSpeedBonus + potData.upgradeMultiplier * (level - 1);
}