using System.Collections.Generic;
using UnityEngine;

//plantData
// plantData = GameManager.Instance.GetSaveData().plantData;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData<List<PlantData>>("PlantData", plantData);  //데이터 저장

public class Collection : MonoBehaviour
{
    public List<PlantData> plantData;
    public GameObject SlotPrefab;
    public Transform Slots;
    
    public void AddColletion(PlantData data)
    {
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
