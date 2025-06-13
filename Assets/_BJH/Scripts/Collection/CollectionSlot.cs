using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSlot : MonoBehaviour
{
    public PlantData plantData;
    public Image PlantImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI OxyProd;

    public void SetSlot(PlantData Data)
    {
        plantData = Data;
        PlantImage.sprite = Data.GrowthSprite[3];
        Name.text = $"{Data.Name} / {Data.Rarity}";
        Description.text = Data.Description;
        OxyProd.text = $"초당 산소 생산량 : {Data.OxygenProd}";
    }
}
