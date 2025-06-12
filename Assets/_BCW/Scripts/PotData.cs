using UnityEngine;
using System.Collections.Generic;


public enum PotGrade // �̰� �̱⿡ �� ���
{
    Common = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
    Mystery = 5
}


[System.Serializable]
public class RarityBonus
{
    public PlantRarity rarity;     // ���
    public float bonusPercent;     // ���� ��ġ
}


[CreateAssetMenu(fileName = "NewPotData", menuName = "ScriptableObjects/PotData")]
public class PotData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string potName;         // ȭ�� �̸�
    public Sprite potIcon;            // �̹���
    public PotGrade rarity;        // ȭ�� ��ü ��� 

    [Header("�Ĺ� ��޺� Ȯ�� ����ġ +,-�ؼ� 100 ������� )")]
    public List<RarityBonus> rarityBonuses = new List<RarityBonus>();

    [Header("�ڵ� ���差")]
    public float growthSpeedBonus = 0f;     

    [Header("Ŭ���� ���差")]
    public float clickPowerBonus = 0f;     

    [Header("��ȭ ������ �ɷ�ġ �߰���")]
    public float upgradeMultiplier = 0.5f;  

    [Header("��ȭ ��� ���� ����")]
    public float upgradeO2Multiplier = 1.5f; 

    [Header("�ִ� ��ȭ ����")]
    public int maxLevel = 10;              
}
