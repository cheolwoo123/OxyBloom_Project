using System.Collections.Generic;
using UnityEngine;

public class PlantCollection : MonoBehaviour
{
    public List<PlantData> PlantDatas; // 전체 리스트
    public GameObject CollectionPrefab;

    //public void Update()
    //{
    //    emissionTimer += Time.deltaTime;

    //    if (emissionTimer >= 1f)
    //    {
    //        OxygenEmission();
    //    }
    //}

    public void Start()
    {
        
    }

    public void SetCollection()
    {
        if (CollectionPrefab != null)
        {
            for (int i = 0; i < PlantDatas.Count; i++)
            {
                Instantiate(CollectionPrefab, this.transform);

                GameObject obj = CollectionPrefab;
                obj.GetComponent<Collider>().enabled = false;

            }
        }
    }
}