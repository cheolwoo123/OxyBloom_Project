using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PotGachaSystem : MonoBehaviour
{
    //public PotInventory potInventory; 
    public int GachaCost = 300; // 가챠 비용

    private List<PotData> allPotList; 

    // 가챠샵 확률 무조건 총합 100
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
        // Resources/PotData 안에 폴더에서 모든 데이터 로드함
        allPotList = Resources.LoadAll<PotData>("PotData").ToList();

        
    }

    // 
    public void TryGacha()
    {
        // 산소쓸때 
        //if (potInventory.player.oxygen < drawCost) return;
        //potInventory.player.oxygen -= drawCost;

        // 무작위 PotData 선택
        PotData GachaData = GetRandomPot();
        if (GachaData == null) return;

        // 인스턴스 생성 후 인벤토리에 추가
        var newPot = new PotInstance(GachaData);
        //potInventory.AddPot(newPot);
    }

    // 등급에 따라 무작위 선택
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

    // 등급에 맞는 PotData 중 랜덤 선택
    private PotData GetRandomPot()
    {
        PotGrade grade = GetRandomGrade();
        var candidates = allPotList.FindAll(p => p.rarity == grade);
        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }
}
