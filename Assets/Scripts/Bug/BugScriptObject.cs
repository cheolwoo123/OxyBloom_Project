using UnityEngine;

public enum PestType
{
    None,
    PlantDegrowth,
    PlantDestruct,
    OxygenLooter
}



[CreateAssetMenu(fileName = "NewBug", menuName = "ScriptableObjects/Bug")]
public class BugScriptObject : ScriptableObject
{

    [Header("Pest Bug")]
    public PestType pestType;    

    [Header("Stat")]
    public float maxHP = 3;
    public int bugStack = 1;
    public int growUp = 10;
    public float speed = 1;
    public int oxygenAmount;
}
