using System;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public List<PlantData> plantData;
    public GameObject SlotPrefab;
    public Transform Slots;

    private void Start()
    {
        plantData = GameManager.Instance.GetSaveData().plantData;
    }

    public void AddColletion(PlantData data)
    {
        if (plantData.Contains(data)) return; // 중복 추가 방지

        plantData.Add(data);
        
        if (SlotPrefab != null)
        {
            Instantiate(SlotPrefab, Slots.transform);

            GameObject obj = SlotPrefab;
            CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
            collectionSlot.SetSlot(data);
        }
    }
}
//plantData
// plantData = GameManager.Instance.GetSaveData().plantData;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData("PlantData", plantData);  //데이터 저장