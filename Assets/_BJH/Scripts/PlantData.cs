using System;
using UnityEngine;

public enum PlantRarity
{
    Common, // 평범 50%
    Rare, // 희귀 25%
    Epic, // 서사 15%
    Legend, // 전설 7.5%
    Mystery  // 신비 2.5%
}

[Serializable]
[CreateAssetMenu(fileName = "Plant_", menuName = "Plant Data")]
public class PlantData : ScriptableObject
{
    [Header("희귀도")]
    public PlantRarity Rarity;

    [Header("이름과 설명")]
    public string Name;
    public string Description;

    [Header("산소, 에너지 생산량")]
    public int OxygenProd;
    public int EnergyProd;

    [Header("성장 요구치")]
    public int GrowthCost;

    [Header("식물 외형")]
    public Sprite[] GrowthSprite = new Sprite[4];
}
