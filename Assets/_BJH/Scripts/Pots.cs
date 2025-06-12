using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Pots : MonoBehaviour
{
    [Header("현재 식물 데이터와 이미지")]
    public PlantData Plant = null; // 현재 식물 데이터
    public Image PlantImg = null; // 현재 식물 이미지

    [Header("식물 성장치와 다음 성장 단계")]
    public int GrowthPower = 0; // 현재 식물 성장치
    public int GrowthStage = 0; // 현재 식물 성장 단계

    Dictionary<PlantRarity, float> rarityChances = new Dictionary<PlantRarity, float>()
    {
        { PlantRarity.Common, 75f },
        { PlantRarity.Rare, 15f },
        { PlantRarity.Epic, 7.5f },
        { PlantRarity.Legend, 2f },
        { PlantRarity.Mystery, 0.5f }
    };

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

    //public void plantingSeed()
    //{
    //    if (Plant != null) return;

    //    PlantRarity targetRarity = GetRandomRarity();

    //    List<PlantData> allPlants = GameManager.Instance.PlantCollection.PlantDatas;


    //    List<PlantData> targetPlants = allPlants.Where(p => p.Rarity == targetRarity).ToList();

    //    if (targetPlants.Count == 0)
    //    {
    //        Debug.LogWarning($"희귀도 {targetRarity}에 해당하는 식물이 없습니다.");
    //        return;
    //    }

    //    // 후보 중 무작위 선택
    //    Plant = targetPlants[Random.Range(0, targetPlants.Count)];

    //    Debug.Log($"{Plant.Name}/{Plant.Rarity}을 심었습니다!");
    //    PlantImg.enabled = true;
    //    PlantImg.sprite = Plant.GrowthSprite[0];
    //    GrowthStage++;
    //}


    public void GrowPlant(int amount)
    {
        Debug.Log($"식물을 {amount}만큼 성장시켰습니다.");
        GrowthPower += amount;
        NextGrowthSprite();
    }

    private void NextGrowthSprite()
    {
        if (PlantImg.enabled != true) PlantImg.enabled = true;

        if (GrowthPower >= Plant.GrowthCost)
        {
            GrowthPower = 0;
            GrowthStage++;
            PlantImg.sprite = Plant.GrowthSprite[GrowthStage];
        }
    }

    public void ClearPot()
    {
        Plant = null;
        PlantImg = null;
        PlantImg.enabled = false;
        GrowthPower = 0;
        GrowthStage = 0;
    }
}



