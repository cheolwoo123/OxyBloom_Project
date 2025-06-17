using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("현재 데이터와 스프라이트")]
    public PlantData plantData = null; // 현재 식물 데이터
    public SpriteRenderer PlantSpr = null; // 현재 식물 스프라이트

    [Header("현재 성장치와 성장 단계")]
    public float CurGrow = 0; // 현재 식물 성장치
    public int GrowthStage = 0; // 현재 식물 성장 단계

    public Animator animator;

    public void Start()
    {
        LoadPlantData();

        GameManager.Instance.uiManager.DisplayPlantButton();
    }

    public void Seeding(PlantData Data)
    {
        plantData = Data;
        PlantSpr.enabled = true;
        PlantSpr.sprite = plantData.GrowthSprite[0];

        SavePlantData();
    }
    
    public void GrowPlant(float amount) // 식물 성장
    {
        if (plantData == null || GrowthStage == 3) return;
        
        Debug.Log($"성장 {CurGrow} + {amount}");
        CurGrow += amount;
        GameManager.Instance.plantManager.growthGauge.UpdateGauge();
        NextGrowthSprite();

        SavePlantData();
    }

    public void DegrowPlant(float amount)
    {
        if (plantData == null || GrowthStage == 3) return;

        Debug.Log($"시듦 {CurGrow} - {amount}");
        CurGrow -= amount;
        GameManager.Instance.plantManager.growthGauge.UpdateGauge();

        SavePlantData();
    }

    private void NextGrowthSprite() // 식물 외형 변화
    {
        if (PlantSpr.enabled != true) PlantSpr.enabled = true;

        if (CurGrow >= plantData.GrowthCost)
        {
            CurGrow = 0;
            GrowthStage++;
            animator.SetTrigger("Grow");

            SavePlantData();

            if (GrowthStage == 3)
            {
                PlantSpr.sprite = plantData.GrowthSprite[GrowthStage];
                GameManager.Instance.uiManager.colletionUI.GetComponent<Collection>().AddColletion(plantData);
                GameManager.Instance.uiManager.DisplaySheifButton();
            }
            else
            {
                PlantSpr.sprite = plantData.GrowthSprite[GrowthStage];
            }
        }
    }

    public void PutPlantOnSheif()
    {
        if (GameManager.Instance.plantManager.plantShelf.GetEmptyIndex() == -1) return; //선반에 식물이 꽉 찼을 때

        GameManager.Instance.plantManager.plantShelf.AddToShelf(plantData);
        GameManager.Instance.plantManager.growthGauge.UpdateGauge();
        GameManager.Instance.uiManager.DisplaySheifButton();
        RemovePlant();
    }

    public void RemovePlant()
    {
        PlantSpr.enabled = false;
        PlantSpr.sprite = null;
        plantData = null;
        CurGrow = 0;
        GrowthStage = 0;

        SavePlantData();

        GameManager.Instance.uiManager.DisplayPlantButton();
    }

    private void LoadPlantData()
    {
        if (GameManager.Instance.GetSaveData() == null) return;
        plantData = GameManager.Instance.GetSaveData().plant;  // 데이터
        CurGrow = GameManager.Instance.GetSaveData().curGrow;  // 성장치
        GrowthStage = GameManager.Instance.GetSaveData().growthStage;   // 성장 단계
    }

    private void SavePlantData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData("Plant", plantData);  // 데이터
        GameManager.Instance.saveLoadManager.SetSaveData("CurGrow", CurGrow);  // 성장치
        GameManager.Instance.saveLoadManager.SetSaveData("GrowthStage", GrowthStage);  // 성장 단계
    }
}
