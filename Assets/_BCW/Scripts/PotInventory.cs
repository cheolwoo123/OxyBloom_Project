using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private List<PotInstance> potInventory = new();

    public PlayerStat player = new();    

    private List<GameObject> slotObjects = new();

    private void Start()
    {
        potInventory = GameManager.Instance.GetSaveData().potInventory;

        RefreshUI();
    }
    
    public void RefreshUI()
    {
        foreach (var slot in slotObjects)
            Destroy(slot);
        slotObjects.Clear();
        if (potInventory == null || potInventory.Count == 0)
            return; 
        foreach (var pot in potInventory)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<PotSlotUI>();
            slot.Init(pot, player, this);
            slotObjects.Add(go);
        }

       
    }

    public void AddPot(PotInstance pot)
    {
        foreach (var existing in potInventory)
        {
            if (existing.potData == pot.potData)
            {
                Debug.Log("�ߺ��� ���� " + pot.potData.potName);
                return;
            }
        }

        potInventory.Add(pot);
        GameManager.Instance.saveLoadManager.SetSaveData("PotInstance", potInventory);  //데이터 저장
        RefreshUI();
    }
}
//potInventory
// potInventory = GameManager.Instance.GetSaveData().potInventory;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData("PotInstance", potInventory);  //데이터 저장