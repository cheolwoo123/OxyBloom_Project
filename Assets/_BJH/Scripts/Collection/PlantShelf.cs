using System.Collections.Generic;
using UnityEngine;

public class PlantShelf : MonoBehaviour
{
    public List<PlantData> plantDatas;

    public SpriteRenderer[] ShelfSpr = new SpriteRenderer[4];

    private float emissionTimer = 0f; // 산소 생산 타이머

    public void Start()
    {
        UpdateShelf();
    }

    public void Update()
    {
        if (plantDatas == null) return;

        emissionTimer += Time.deltaTime;

        if (emissionTimer >= 1f)
        {
            OxygenEmission();
        }
    }

    private void OxygenEmission()
    {
        foreach (var plantData in plantDatas)
        {
            if (plantData != null)
            {
                GameManager.Instance.SetOxygen(plantData.OxygenProd);
            }
        }
        emissionTimer = 0f;
    }

    public void AddToShelf(PlantData data)
    {
        if (plantDatas.Count == 4)
        {
            Debug.LogWarning("선반이 가득 찼습니다.");
            return;
        }

        plantDatas.Add(data);
        UpdateShelf();
    }

    private void UpdateShelf()
    {
        for (int i = 0; i < plantDatas.Count; i++)
        {
            if (plantDatas[i] != null)
            {
                ShelfSpr[i].sprite = plantDatas[i].GrowthSprite[3];
            }
            else
            {
                ShelfSpr[i].sprite = null; 
            }
        }
    }

    public void ClearShelf(int index)
    {
        if (plantDatas[index] == null) return;

        plantDatas[index] = null;
        UpdateShelf();

    }

    private void LoadPlantData()
    {
        if (GameManager.Instance.GetSaveData().plantDatas != null)
        {
            plantDatas = GameManager.Instance.GetSaveData().plantDatas;
        }
    }

    public void SavePlantData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData<List<PlantData>>("PlantDatas", plantDatas);
    }
}