




using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public List<PotInstance> potInventory = new();
    public PotInstance equippedPot;
    public float oxygen = 1000f;
    public PlantManager  plantManager;
    private int _oxygen;
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)
    {
        Oxygen = Oxygen + i;
        GameManager.Instance.uiManager.Oxygen(Oxygen);
    }

    public void EquipPot(PotInstance pot)
    {
        equippedPot = pot;
    }

    public bool UpgradePot(PotInstance pot)
    {
        int cost = Mathf.FloorToInt(100 * Mathf.Pow(pot.potData.upgradeO2Multiplier, pot.level - 1));
        if (oxygen < cost || pot.level >= pot.potData.maxLevel)
            return false;

        oxygen -= cost;
        pot.level++;
        return true;
    }


    public float GetTotalGrowthBonus() => equippedPot != null ? equippedPot.GetGrowthBonus() : 0f;
}
