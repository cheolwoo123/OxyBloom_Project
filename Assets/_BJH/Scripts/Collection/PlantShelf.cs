using System.Collections.Generic;
using UnityEngine;

public class PlantShelf : MonoBehaviour
{
    public PlantData[] plantDatas = new PlantData[4];

    public SpriteRenderer[] ShelfSpr = new SpriteRenderer[4];

    private float emissionTimer = 0f; // 산소 생산 타이머

    public void Start()
    {
        plantDatas = GameManager.Instance.GetSaveData().plantDatas;
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
        for (int i = 0; i < plantDatas.Length; i++)
        {
            if (plantDatas[i] == null)
            {
                plantDatas[i] = data;
                break;
            }
        }
        GameManager.Instance.saveLoadManager.SetSaveData("PlantDatas", plantDatas);
        UpdateShelf();
    }

    private void UpdateShelf()
    {
        for (int i = 0; i < plantDatas.Length; i++)
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
        GameManager.Instance.saveLoadManager.SetSaveData("PlantDatas", plantDatas);
        UpdateShelf();

    }
}

//plantDatas
// plantDatas = GameManager.Instance.GetSaveData().plantDatas;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData("PlantDatas", plantDatas);  //데이터 저장