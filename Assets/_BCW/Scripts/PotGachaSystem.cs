// ? PotGachaSystem.cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotGachaSystem : MonoBehaviour
{
    public PlayerData playerData;
    public PotInventory inventory;
    public int drawCost = 300;
    private List<PotData> allPotList;

    private Dictionary<PotGrade, float> gradeChances = new()
    {
        { PotGrade.Common, 50f },
        { PotGrade.Rare, 30f },
        { PotGrade.Epic, 12f },
        { PotGrade.Legendary, 7f },
        { PotGrade.Mystery, 1f }
    };

    private void Awake()
    {
        allPotList = Resources.LoadAll<PotData>("PotData").ToList();
    }

    public void TryDraw()
    {
        // 산소가 부족하면 뽑기 실패 (return)
        // if (playerData.oxygen < drawCost) return;
        Debug.Log("산소부족");

        // 뽑기 비용만큼 산소 차감
       // playerData.oxygen -= drawCost;
       // Debug.Log($"산소 차감: {drawCost} 산소, 남은 산소: {playerData.oxygen}");

        // 확률에 따라 무작위 PotData 선택
        PotData drawnData = GetRandomPot();
        if (drawnData == null) return; // 실패 시 리턴

        // 뽑은 PotData로 PotInstance 생성 (레벨 1)
        var newPot = new PotInstance(drawnData);

        // 플레이어 인벤토리에 추가
       // playerData.potInventory.Add(newPot);

        // 인벤토리 UI 새로고침
        inventory.RefreshUI();
    }

    private PotGrade GetRandomGrade()
    {
        float roll = Random.Range(0f, 100f);
        float acc = 0f;
        foreach (var pair in gradeChances)
        {
            acc += pair.Value;
            if (roll <= acc) return pair.Key;
        }
        return PotGrade.Common;
    }

    private PotData GetRandomPot()
    {
        PotGrade grade = GetRandomGrade();
        var candidates = allPotList.FindAll(p => p.rarity == grade);
        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }
}
