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
        LoadCollectionData();
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

        SaveCollectionData();
    }

    private void SaveCollectionData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData("PlantData", plantData);
    }

    public void LoadCollectionData()
    {
        if (GameManager.Instance.GetSaveData().plantData != null)
        {
            plantData = GameManager.Instance.GetSaveData().plantData;

            if (SlotPrefab != null)
            {
                Instantiate(SlotPrefab, Slots.transform);

                GameObject obj = SlotPrefab;
                CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
                collectionSlot.SetSlot(data);
            }
        }
        else
        {
            plantData = new List<PlantData>();
        }
    }
}
