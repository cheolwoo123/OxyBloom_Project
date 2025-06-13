using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    

    public PlayerData player = new();

    [Header("Tooltip Reference")]
    public GameObject tooltipPanel;
    

    private List<GameObject> slotObjects = new();

    public void RefreshUI()
    {
        foreach (var slot in slotObjects)
            Destroy(slot);
        slotObjects.Clear();

        foreach (var pot in player.potInventory)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<PotSlotUI>();
            slot.Init(pot, player, this);
            slotObjects.Add(go);
        }

       
    }

    public void AddPot(PotInstance pot)
    {
        player.potInventory.Add(pot);
        RefreshUI();
    }
}