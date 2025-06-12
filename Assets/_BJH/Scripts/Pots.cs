using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    [Header("현재 식물 데이터와 이미지")]
    public PlantData PlantData = null; // 현재 식물 데이터
    public SpriteRenderer PlantSpr = null; // 현재 식물 이미지

    [Header("식물 성장치와 다음 성장 단계")]
    public int CurGrow = 0; // 현재 식물 성장치
    public int GrowthStage = 0; // 현재 식물 성장 단계

    private float emissionTimer = 0f; // 산소 생산 타이머

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

    public void Update()
    {
        emissionTimer += Time.deltaTime;

        if (emissionTimer >= 1f)
        {
            OxygenEmission();
        }
    }

    public void OxygenEmission()
    {
        //GameManager.Instance.SetOxyzen(PlantData.OxygenProd)
        emissionTimer = 0f;
    }

    //public void plantingSeed() // 무작위 식물 심기
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


    public void GrowPlant(int amount) // 식물 성장
    {
        Debug.Log($"식물을 {amount}만큼 성장시켰습니다.");
        CurGrow += amount;
        NextGrowthSprite();
    }

    private void NextGrowthSprite() // 식물 외형 변화
    {
        if (PlantSpr.enabled != true) PlantSpr.enabled = true;

        if (CurGrow >= PlantData.GrowthCost)
        {
            CurGrow = 0;
            GrowthStage++;
            PlantSpr.sprite = PlantData.GrowthSprite[GrowthStage];
        }
    }

    public void ClearPot() // 화분 정리
    {
        emissionTimer = 0f;
        PlantData = null;
        PlantSpr = null;
        PlantSpr.enabled = false;
        CurGrow = 0;
        GrowthStage = 0;
    }
}



