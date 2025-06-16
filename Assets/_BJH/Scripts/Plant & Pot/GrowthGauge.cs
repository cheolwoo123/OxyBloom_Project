using UnityEngine;
using UnityEngine.UI;

public class GrowthGauge : MonoBehaviour
{
    public Image GrowGauge;

    public void UpdateGauge()
    {
        Pot pot = GameManager.Instance.plantManager.pot;

        GrowGauge.fillAmount = pot.GetPlant().CurGrow / pot.GetPlant().plantData.GrowthCost;
    }
}
