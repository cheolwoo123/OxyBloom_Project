//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class PotSlotUI : MonoBehaviour
//{
//    public Image iconImage;
//    public TextMeshProUGUI nameText;
//    public TextMeshProUGUI levelText;
//    public TextMeshProUGUI equippedText;
//    public Button equipButton;
//    public Button upgradeButton;

//    private PotInstance pot;
//    private PlayerData player;
//    private PotInventory inventoryUI;

//    public void Init(PotInstance pot, PlayerData player, PotInventory inventoryUI)
//    {
//        this.pot = pot;
//        this.player = player;
//        this.inventoryUI = inventoryUI;

//        iconImage.sprite = pot.potData.potIcon;
//        nameText.text = pot.potData.potName;
//        levelText.text = $"Lv.{pot.level}";

//        equipButton.onClick.RemoveAllListeners();
//        equipButton.onClick.AddListener(() => {
//            player.EquipPot(pot);
//            inventoryUI.RefreshUI();
//        });

//        upgradeButton.onClick.RemoveAllListeners();
//        upgradeButton.onClick.AddListener(() => {
//            if (player.UpgradePot(pot))
//            {
//                levelText.text = $"Lv.{pot.level}";
//            }
//        });

//        equippedText.gameObject.SetActive(player.equippedPot == pot);
//    }
//}
