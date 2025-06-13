using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("현재 식물 데이터와 이미지")]
    public PotData potData = null; // 현재 화분 데이터
    public SpriteRenderer PotSpr; // 현재 화분 외형
    private Plant plant;

    Dictionary<PlantRarity, float> rarityChances = new Dictionary<PlantRarity, float>();

    PlantRarity GetRandomRarity()
    {
        float roll = Random.Range(0f, 100f);
        float chance = 0f; // 식물에 확률이 들어갈 변수

        foreach (var pair in rarityChances)
        {
            chance += pair.Value;

            if (roll <= chance)
            {
                return pair.Key;
            }
        }
        return PlantRarity.Common;
    }

    public void ChangePot(PotData Data)
    {
        potData = Data;
        UpdataChance();
    }

    public void Awake()
    {
        UpdataChance();
    }

    public void Start()
    {
        ChangeSprite();
        
    }

    public void plantingRandomSeed() // 무작위 식물 심기 (버튼 연결)
    {
        if (plant.plantData != null) return;
        
        PlantRarity targetRarity = GetRandomRarity();

        List<PlantData> allPlants = GameManager.Instance.plantManager.PlantDatas;

        List<PlantData> targetPlants = allPlants.Where(p => p.Rarity == targetRarity).ToList();

        if (targetPlants.Count == 0)
        {
            Debug.LogWarning($"희귀도 {targetRarity}에 해당하는 식물이 없습니다.");
            return;
        }

        plant.plantData = targetPlants[Random.Range(0, targetPlants.Count)];
        plant.Seeding(plant.plantData);

        Debug.Log($"{plant.plantData.Name}/{plant.plantData.Rarity}을 심었습니다!");
    }

    public void UpdataChance()
    {
        rarityChances.Clear();

        rarityChances[PlantRarity.Common] = potData?.CommonChance ?? 0f;
        rarityChances[PlantRarity.Rare] = potData?.RareChance ?? 0f;
        rarityChances[PlantRarity.Epic] = potData?.EpicChance ?? 0f;
        rarityChances[PlantRarity.Legend] = potData?.LegendChance ?? 0f;
        rarityChances[PlantRarity.Mystery] = potData?.MysteryChance ?? 0f;
    }

    public void ClearPot() // 화분 정리
    {
        potData = null;
        PotSpr.sprite = null;
    }

    public void ChangeSprite() // 화분 변경
    {
        PotSpr.sprite = potData.potIcon;
    }

    public Plant GetPlant()
    {
        return plant;
    }
}



