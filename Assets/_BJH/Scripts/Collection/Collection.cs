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
            plantData = GameManager.Instance.GetSaveData().plantData;

            foreach (Transform child in Slots.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < plantData.Count; i++)
            {

                if (SlotPrefab != null)
                {
                    GameObject obj = Instantiate(SlotPrefab, Slots.transform);
                    CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
                    collectionSlot.SetSlot(plantData[i]);
                }
            }
        }
    }
}
