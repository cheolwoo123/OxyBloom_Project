using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("현재 식물 데이터와 이미지")]
    public PlantData PlantData = null; // 현재 식물 데이터
    //public PotData PotData = null; // 현재 화분 데이터
    private Plant Plant;

    //private Dictionary<PlantRarity, float> rarityChances = new Dictionary<PlantRarity, float>()
    //{
    //    { PlantRarity.Common, PotData.CommonChance },
    //    { PlantRarity.Rare,  PotData.RareChance },
    //    { PlantRarity.Epic,  PotData.EpicChance },
    //    { PlantRarity.Legend,  PotData.LegendChance },
    //    { PlantRarity.Mystery,  PotData.MysteryChance }
    //};

    //PlantRarity GetRandomRarity()
    //{
    //    float roll = Random.Range(0f, 100f);
    //    float chance = 0f; // 식물에 확률이 들어갈 변수

    //    foreach (var pair in rarityChances)
    //    {
    //        chance += pair.Value;

    //        if (roll <= chance)
    //        {
    //            return pair.Key;
    //        }
    //    }
    //    return PlantRarity.Common;
    //}

    public void Start()
    {
        Plant = GetComponentInChildren<Plant>();
    }

    public void plantingRandomSeed() // 무작위 식물 심기 (버튼 연결)
    {
        //if (Plant != null) return;

        //PlantRarity targetRarity = GetRandomRarity();

        //List<PlantData> allPlants = GameManager.Instance.PlantCollection.PlantDatas;

        //List<PlantData> targetPlants = allPlants.Where(p => p.Rarity == targetRarity).ToList();

        //if (targetPlants.Count == 0)
        //{
        //    Debug.LogWarning($"희귀도 {targetRarity}에 해당하는 식물이 없습니다.");
        //    return;
        //}

        // 후보 중 무작위 선택
        //PlantData = targetPlants[Random.Range(0, targetPlants.Count)];
        //Plant.Seeding(PlantData);

        //Debug.Log($"{PlantData.Name}/{PlantData.Rarity}을 심었습니다!");
    }

    public void ClearPot() // 화분 정리
    {
        PlantData = null;
    }
}



