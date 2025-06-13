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

    public void OxygenEmission()
    {
        foreach (var plantData in plantDatas)
        {
            GameManager.Instance.SetOxygen(plantData.OxygenProd);
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
        if (plantDatas == null) return;

        for (int i = 0; i < plantDatas.Count; i++)
        {
            ShelfSpr[i].sprite = plantDatas[i].GrowthSprite[3];
        }
    }

    public void ClearShelf(int index)
    {
        plantDatas[index] = null;
        UpdateShelf();
    }
}
