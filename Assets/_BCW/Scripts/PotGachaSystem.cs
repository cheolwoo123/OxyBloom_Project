using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PotGachaSystem : MonoBehaviour
{
    public PotInventory potInventory; 
    public int GachaCost = 300; // ��í ���

    private List<PotData> allPotList; 

    // ��í�� Ȯ�� ������ ���� 100
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
        // Resources/PotData �ȿ� �������� ��� ������ �ε���
        allPotList = Resources.LoadAll<PotData>("PotData").ToList();

        
    }

    // 
    public void TryGacha()
    {
        // ��Ҿ��� 
        //if (potInventory.player.oxygen < drawCost) return;
        //potInventory.player.oxygen -= drawCost;

        // ������ PotData ����
        PotData GachaData = GetRandomPot();
        if (GachaData == null) return;

        // �ν��Ͻ� ���� �� �κ��丮�� �߰�
        var newPot = new PotInstance(GachaData);
        potInventory.AddPot(newPot);
    }

    // ��޿� ���� ������ ����
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

    // ��޿� �´� PotData �� ���� ����
    private PotData GetRandomPot()
    {
        PotGrade grade = GetRandomGrade();
        var candidates = allPotList.FindAll(p => p.rarity == grade);
        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }
}
