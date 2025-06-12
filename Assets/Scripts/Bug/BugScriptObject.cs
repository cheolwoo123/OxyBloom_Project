using UnityEngine;

public enum BugCategory
{
    Pest,
    Beneficial
}

public enum PestType
{
    None,
    PlantDegrowth,
    PlantDestruct,
    KillBeneficial
}

public enum BeneficialType
{
    None,
    PromoteGrowth,
    ControlOxygen
}

[CreateAssetMenu(fileName = "NewBug", menuName = "ScriptableObjects/Bug")]
public class BugScriptObject : ScriptableObject
{
    [Header("Bug Visual")]
    [SerializeField] public Sprite bugSprite;

    [Header("Bug Type")]
    public BugCategory category;

    [Header("Pest Bug")]
    public PestType pestType;    

    [Header("Beneficial Bug")]
    public BeneficialType beneficialType;

    [Header("Stat")]
    public int maxHP = 3;
    public int damage = 1;
    public int growUp = 1;
    public int speed = 1;

    [Header("Oxygen Control (Option)")]
    public int oxygenAmount;
}
