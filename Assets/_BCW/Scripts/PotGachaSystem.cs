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
        // ��Ұ� �����ϸ� �̱� ���� (return)
        // if (playerData.oxygen < drawCost) return;
        Debug.Log("��Һ���");

        // �̱� ��븸ŭ ��� ����
       // playerData.oxygen -= drawCost;
       // Debug.Log($"��� ����: {drawCost} ���, ���� ���: {playerData.oxygen}");

        // Ȯ���� ���� ������ PotData ����
        PotData drawnData = GetRandomPot();
        if (drawnData == null) return; // ���� �� ����

        // ���� PotData�� PotInstance ���� (���� 1)
        var newPot = new PotInstance(drawnData);

        // �÷��̾� �κ��丮�� �߰�
       // playerData.potInventory.Add(newPot);

        // �κ��丮 UI ���ΰ�ħ
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
