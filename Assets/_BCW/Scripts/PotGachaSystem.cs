using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PotGachaSystem : MonoBehaviour
{
    public int drawCost = 300;
    
    private List<PotData> allPotList;

    private Dictionary<PotGrade, float> gradeChances = new Dictionary<PotGrade, float>()
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
        PotData drawn = GetRandomPot();
        Debug.Log($"»Ì±â {drawn?.potName ?? "¾øÀ½"}");
       
    }

    private PotGrade GetRandomGrade()
    {
        float roll = Random.Range(0f, 100f);
        float acc = 0f;

        foreach (var pair in gradeChances)
        {
            acc += pair.Value;
            if (roll <= acc)
                return pair.Key;
        }

        return PotGrade.Common;
    }

    private PotData GetRandomPot()
    {
        PotGrade grade = GetRandomGrade();
        var candidates = allPotList.FindAll(p => p.rarity == grade);
        if (candidates == null || candidates.Count == 0)
            return null;

        return candidates[Random.Range(0, candidates.Count)];
    }
}
