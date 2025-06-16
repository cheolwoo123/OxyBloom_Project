using System.Collections.Generic;
using UnityEngine;
using TMPro;

//potInventory
// if (GameManager.Instance.saveLoadManager.Load() != null)   //데이터 로드
// {
//     potInventory = GameManager.Instance.saveLoadManager.Load().potInventory;
// }
//GameManager.Instance.saveLoadManager.SetSaveData<List<PotInstance>>("PotInstance", potInventory);  //데이터 저장

public class PotInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField]private List<PotInstance> potInventory = new();

    public PlayerStat player = new();    

    private List<GameObject> slotObjects = new();

    public void RefreshUI()
    {
        foreach (var slot in slotObjects)
            Destroy(slot);
        slotObjects.Clear();

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
        RefreshUI();
    }
}