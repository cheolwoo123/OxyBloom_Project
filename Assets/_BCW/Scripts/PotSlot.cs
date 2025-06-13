using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotSlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI equippedText;
    public TextMeshProUGUI description;
    public Button equipButton;
    public Button upgradeButton;
    private PotInstance pot;


    // 슬롯 UI 초기화
    //public void Init(PotInstance pot, PlayerStat player, PotInventory potInventory)
    //{
    //    this.pot = pot;

    //    iconImage.sprite = pot.potData.potIcon;  
    //    nameText.text = pot.potData.potName;     
    //    levelText.text = $"Lv.{pot.level}";     

    //    UpdateDescription(); // 설명 텍스트 갱신

    //    // 기존 버튼 리스너 제거 후 새로 등록
    //    equipButton.onClick.RemoveAllListeners();
    //    upgradeButton.onClick.RemoveAllListeners();

    //    // 장착 버튼 클릭 시 해당 화분을 장착 후 인벤토리 UI를 갱신
    //    equipButton.onClick.AddListener(() => {
    //        player.EquipPot(pot);
    //        potInventory.RefreshUI();
    //    });

        
    //    // 강화 했을때 레벨이랑 생산퍼센트 갱신
    //    upgradeButton.onClick.AddListener(() => {
    //        if (player.UpgradePot(pot))
    //        {
    //            levelText.text = $"Lv.{pot.level}";
    //            UpdateDescription();
    //        }
    //    });

    //    // 장착시 E표시
    //    equippedText.gameObject.SetActive(player.equippedPot == pot);
    //}

    // 화분마다 식물 뽑기 확률 표시
    private void UpdateDescription()
    {
        description.text =
            $"- 자동 산소 생산량 : {pot.GetGrowthBonus()}%\n" +
            $" - 일반 : {pot.potData.CommonChance}%\n" +
            $" - 레어 : {pot.potData.RareChance}%\n" +
            $" - 에픽 : {pot.potData.EpicChance}%\n" +
            $" - 전설 : {pot.potData.LegendChance}%\n" +
            $" - 미스터리 : {pot.potData.MysteryChance}%";
    }
}
