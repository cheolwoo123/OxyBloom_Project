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
    [Header("Bug Type")]
    public BugCategory category;

    [Header("Pest Bug")]
    public PestType pestType;    

    [Header("Beneficial Bug")]
    public BeneficialType beneficialType;

    [Header("Stat")]
    public float maxHP = 3;
    public int bugStack = 1;
    public int growUp = 10;
    public float speed = 1;

    [Header("Oxygen Control (Option)")]
    public int oxygenAmount;
}
