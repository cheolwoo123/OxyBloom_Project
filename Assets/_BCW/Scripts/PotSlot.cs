using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PotSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI equippedText;
    public Button equipButton;
    public Button upgradeButton;

    private PotInstance pot;
    private PlayerData player;
    private PotInventory potInventory;

    private GameObject tooltipPanel;
    private TextMeshProUGUI tooltipText;

    public void Init(PotInstance pot, PlayerData player, PotInventory potInventory)
    {
        this.pot = pot;
        this.player = player;
        this.potInventory = potInventory;

        iconImage.sprite = pot.potData.potIcon;
        nameText.text = pot.potData.potName;
        levelText.text = $"Lv.{pot.level}";

        equipButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();

        equipButton.onClick.AddListener(() => { 
            player.EquipPot(pot);
            potInventory.RefreshUI();
        });

        upgradeButton.onClick.AddListener(() => {
            if (player.UpgradePot(pot))
            {
                levelText.text = $"Lv.{pot.level}";
            }
        });

        equippedText.gameObject.SetActive(player.equippedPot == pot);

        tooltipPanel = potInventory.tooltipPanel;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPanel == null || tooltipText == null) return;

        tooltipPanel.SetActive(true);
        tooltipText.text = $"{pot.potData.potName}\n성장: {pot.GetGrowthBonus():F1} / \n등급: {pot.potData.rarity} / Lv.{pot.level}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }
}