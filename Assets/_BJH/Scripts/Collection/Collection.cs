using System;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public List<PlantData> plantData;
    public GameObject SlotPrefab;
    public Transform Slots;

    // private void Start()
    // {
    //     LoadCollectionData();
    // }

    public void AddColletion(PlantData data)
    {
        if (plantData.Contains(data)) return; // 중복 추가 방지

        plantData.Add(data);
        
        if (SlotPrefab != null)
        {
            GameObject obj = Instantiate(SlotPrefab, Slots.transform);
            CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
            collectionSlot.SetSlot(data);
        }

        SaveCollectionData();
    }

    private void SaveCollectionData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData<List<PlantData>>("PlantData", plantData);
    }

    public void LoadCollectionData()
    {
        if (GameManager.Instance.GetSaveData().plantData != null)
        {
            Debug.Log("Collection Load Data");

            plantData = GameManager.Instance.GetSaveData().plantData;

            for (int i = 0; i < plantData.Count; i++)
            {
                if (SlotPrefab != null)
                {
                    Debug.Log($"Instantiate SlotPrefab for {plantData[i].Name}");

                    Instantiate(SlotPrefab, Slots.transform);

                    GameObject obj = SlotPrefab;
                    CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
                    collectionSlot.SetSlot(plantData[i]);
                }
            }
        }
    }
}
