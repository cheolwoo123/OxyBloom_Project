using JetBrains.Annotations;
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
        if (plantData == null)
        {
            GameManager.Instance.uiManager.DisplayPlantButton();
        }
    }

    public void Seeding(PlantData Data)
    {
        plantData = Data;
        PlantSpr.enabled = true;
        PlantSpr.sprite = plantData.GrowthSprite[0];
    }
    
    public void GrowPlant(float amount) // 식물 성장
    {
        if (plantData == null || GrowthStage == 3) return;
        
        Debug.Log($"성장 {CurGrow} + {amount}");
        CurGrow += amount;
        NextGrowthSprite();
    }

    public void DegrowPlant(float amount)
    {
        if (plantData == null) return;

        Debug.Log($"시듦 {CurGrow} - {amount}");
        CurGrow -= amount;
    }

    private void NextGrowthSprite() // 식물 외형 변화
    {
        if (PlantSpr.enabled != true) PlantSpr.enabled = true;

        if (CurGrow >= plantData.GrowthCost)
        {
            CurGrow = 0;
            GrowthStage++;
            animator.SetTrigger("Grow");

            if (GrowthStage == 3)
            {
                PlantSpr.sprite = plantData.GrowthSprite[GrowthStage];
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
        GameManager.Instance.plantManager.plantShelf.AddToShelf(plantData);
        GameManager.Instance.uiManager.colletionUI.GetComponent<Collection>().AddColletion(plantData);
        RemovePlant();
        GameManager.Instance.uiManager.DisplaySheifButton();
        GameManager.Instance.uiManager.DisplayPlantButton();
    }

    public void RemovePlant()
    {
        PlantSpr.enabled = false;
        PlantSpr.sprite = null;
        plantData = null;
        CurGrow = 0;
        GrowthStage = 0;
    }
}
