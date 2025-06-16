
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    private List<PotInstance> potInventory = new();
    private PlayerStat player = new();
    private float oxygen;
}
