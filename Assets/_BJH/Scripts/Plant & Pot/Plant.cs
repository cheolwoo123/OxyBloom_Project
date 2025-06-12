using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("현재 데이터와 스프라이트")]
    public PlantData PlantData = null; // 현재 식물 데이터
    public SpriteRenderer PlantSpr = null; // 현재 식물 스프라이트

    [Header("현재 성장치와 성장 단계")]
    public int CurGrow = 0; // 현재 식물 성장치
    public int GrowthStage = 0; // 현재 식물 성장 단계

    private float emissionTimer = 0f; // 산소 생산 타이머

    public void Update()
    {
        if (PlantData == null) return;

        emissionTimer += Time.deltaTime;

        if (emissionTimer >= 1f)
        {
            OxygenEmission();
        }
    }

    public void OxygenEmission()
    {
        //gamemanager.instance.setoxyzen(plantdata.oxygenprod)
        emissionTimer = 0f;
    }

    public void Seeding(PlantData Data)
    {
        PlantData = Data;
        PlantSpr.enabled = true;
        PlantSpr.sprite = PlantData.GrowthSprite[0];
        GrowthStage++;
    }
    

    public void GrowPlant(int amount) // 식물 성장
    {
        Debug.Log($"식물을 {amount}만큼 성장시켰습니다.");
        CurGrow += amount;
        NextGrowthSprite();
    }

    private void NextGrowthSprite() // 식물 외형 변화
    {
        if (PlantSpr.enabled != true) PlantSpr.enabled = true;

        if (CurGrow >= PlantData.GrowthCost)
        {
            CurGrow = 0;
            GrowthStage++;
            PlantSpr.sprite = PlantData.GrowthSprite[GrowthStage];
        }
    }
}
