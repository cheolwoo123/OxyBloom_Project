using UnityEngine;
using System.Collections.Generic;


public enum PlantRarity // 이거 합치면 지울거
{
    Common,
    Rare,
    Epic,
    Legend,
    Mystery
}


public enum PotGrade // 이거 뽑기에 쓸 등급
{
    Common = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
    Mystery = 5
}


[System.Serializable]
public class RarityBonus
{
    public PlantRarity rarity;     // 등급
    public float bonusPercent;     // 보정 수치
}


[CreateAssetMenu(fileName = "NewPotData", menuName = "ScriptableObjects/PotData")]
public class PotData : ScriptableObject
{
    [Header("기본 정보")]
    public string potName;         // 화분 이름
    public Sprite potIcon;            // 이미지
    public PotGrade rarity;        // 화분 자체 등급 

    [Header("식물 등급별 확률 보정치 +,-해서 100 맞춰야함 )")]
    public List<RarityBonus> rarityBonuses = new List<RarityBonus>();

    [Header("자동 성장량")]
    public float growthSpeedBonus = 0f;     

    [Header("클릭당 성장량")]
    public float clickPowerBonus = 0f;     

    [Header("강화 레벨당 능력치 추가량")]
    public float upgradeMultiplier = 0.5f;  

    [Header("강화 비용 증가 배율")]
    public float upgradeO2Multiplier = 1.5f; 

    [Header("최대 강화 레벨")]
    public int maxLevel = 10;              
}
