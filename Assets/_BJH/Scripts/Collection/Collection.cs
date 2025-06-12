using UnityEngine;

public class Collection : MonoBehaviour
{
    public GameObject SlotPrefab;
    public Transform Slots;

    public void Start()
    {
        SetCollection();
    }

    public void SetCollection()
    {
        //if (SlotPrefab != null)
        //{
        //    for (int i = 0; i < GameManager.Instance.PlantManager.PlantDatas.Count; i++)
        //    {
        //        Instantiate(SlotPrefab, Slots.transform);

        //        GameObject obj = SlotPrefab;
        //        CollectionSlot collectionSlot = obj.GetComponent<CollectionSlot>();
        //        collectionSlot.SetSlot(GameManager.Instance.PlantManager.PlantDatas[i])
        //    }
        //}
    }

}
