using UnityEngine;

public class PlantShelf : MonoBehaviour
{
    [Header("진열된 식물 데이터")]
    public PlantData[] plantDatas = new PlantData[4];

    [Header("진열된 식물 스프라이트")]
    public SpriteRenderer[] ShelfSpr = new SpriteRenderer[4];

    private float emissionTimer = 0f; // 산소 생산 타이머

    public void Start()
    {
        LoadPlantData();
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
        plantDatas[GetEmptyIndex()] = data;
        UpdateShelf();
        SavePlantData();
    }

    public int GetEmptyIndex() // 진열장 빈 공간 찾기
    {
        for (int i = 0; i < plantDatas.Length; i++)
        {
            if (plantDatas[i] == null)
            {
                return i;
            }
        }
        return -1;
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
        UpdateShelf();
        SavePlantData();
    }

    private void LoadPlantData()
    {
        if (GameManager.Instance.GetSaveData().plantDatas == null) return;

        plantDatas = GameManager.Instance.GetSaveData().plantDatas;
    }

    public void SavePlantData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData<PlantData[]>("PlantDatas", plantDatas);
    }
}