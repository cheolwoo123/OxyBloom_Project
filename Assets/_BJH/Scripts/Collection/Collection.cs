using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public List<PlantData> plantData;
    public GameObject SlotPrefab;
    public Transform Slots;

    public void Start()
    {
        SetCollection();
    }

    public void SetCollection()
    {
        if (SlotPrefab != null)
        {
            for (int i = 0; i < plantData.Count; i++)
            {
                Instantiate(SlotPrefab, Slots.transform);

                GameObject obj = SlotPrefab;
                CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
                collectionSlot.SetSlot(plantData[i]);
            }
        }
    }

    public void AddColletion(PlantData data)
    {
        plantData.Add(data);
        SetCollection();
    }
}
