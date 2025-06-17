
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int oxygen;
    public PlayerStat playerStat = new();
    
    public PotData potData;                //Pot
    
    public PlantData plant;                //Plant
    public float curGrow = 0;
    public int growthStage;
    
    public List<PlantData> plantData;  //Collection
    
    public List<PlantData> plantDatas;  //PlantShelf
    
    public List<PotInstance> potInventory;  //PotInventory

    public int surviveDays;                //RoundManager
}
