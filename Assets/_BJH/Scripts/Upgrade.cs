using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Button PmButton;
    public TextMeshProUGUI PmLevel;
    public TextMeshProUGUI PmCost;

    public Button AtkButton;
    public TextMeshProUGUI AtkLevel;
    public TextMeshProUGUI AtkCost;

    private int pmCost;
    private int atkCost;

    void Start()
    {
        UpdateUpgradeUI();
    }

    public void UpdateUpgradeUI()
    {
        pmCost = GameManager.Instance.player.stat.pmLevel * 300;
        atkCost = GameManager.Instance.player.stat.atkLevel * 300;

        PmLevel.text = $"현재 레벨: {GameManager.Instance.player.stat.pmLevel}";
        PmCost.text = $"- {pmCost}";
        AtkLevel.text = $"현재 레벨: {GameManager.Instance.player.stat.atkLevel}";
        AtkCost.text = $"- {atkCost}";
    }

    public void UpgradeATK()
    {
        if (GameManager.Instance.Oxygen < atkCost)
        {
            GameManager.Instance.StartCoroutine("NotEnoughOxyzen",atkCost);
            return;
        }
        GameManager.Instance.player.stat.ATKLevelUp();
        GameManager.Instance.SetOxygen(-atkCost);
        UpdateUpgradeUI();
    }

    public void UpgradePM()
    {
        if (GameManager.Instance.Oxygen < pmCost)
        {
            GameManager.Instance.StartCoroutine("NotEnoughOxyzen", pmCost);
            return;
        }
        GameManager.Instance.player.stat.PMLevelUp();
        GameManager.Instance.SetOxygen(-pmCost);
        UpdateUpgradeUI();
    }
}
