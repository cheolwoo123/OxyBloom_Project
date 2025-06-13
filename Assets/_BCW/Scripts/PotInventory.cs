using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private PlayerData playerData;
    public List<PotInstance> testPotList = new();
    private List<GameObject> slotObjects = new();

    public void RefreshUI()
    {
        foreach (var slot in slotObjects)
            Destroy(slot);
        slotObjects.Clear();

        foreach (var pot in testPotList)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<PotSlotUI>();
            slot.Init(pot, null, this);
            slotObjects.Add(go);
        }

        inventoryText.text = $"Inventory {testPotList.Count}";
    }
}