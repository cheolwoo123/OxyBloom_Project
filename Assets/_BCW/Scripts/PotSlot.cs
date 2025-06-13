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


    // ���� UI �ʱ�ȭ
    public void Init(PotInstance pot, PlayerStat player, PotInventory potInventory)
    {
        this.pot = pot;

        iconImage.sprite = pot.potData.potIcon;  
        nameText.text = pot.potData.potName;     
        levelText.text = $"Lv.{pot.level}";     

        UpdateDescription(); // ���� �ؽ�Ʈ ����

        // ���� ��ư ������ ���� �� ���� ���
        equipButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();

        // ���� ��ư Ŭ�� �� �ش� ȭ���� ���� �� �κ��丮 UI�� ����
        equipButton.onClick.AddListener(() => {
            GameManager.Instance.plantManager.pot.ChangePot(pot.potData);
            
            potInventory.RefreshUI();
        });


        // ��ȭ ������ �����̶� �����ۼ�Ʈ ����
        upgradeButton.onClick.AddListener(() =>
        {
            if (player.UpgradePot(pot))
            {
                levelText.text = $"Lv.{pot.level}";
                UpdateDescription();
            }
        });

        // ������ Eǥ��
        //equippedText.gameObject.SetActive(GameManager.Instance.plantManager.pot.potData = pot.potData);
    }

    // ȭ�и��� �Ĺ� �̱� Ȯ�� ǥ��
    private void UpdateDescription()
    {
        description.text =
            $"- �ڵ� ��� ���귮 : {pot.GetGrowthBonus()}%\n" +
            $" - �Ϲ� : {pot.potData.CommonChance}%\n" +
            $" - ���� : {pot.potData.RareChance}%\n" +
            $" - ���� : {pot.potData.EpicChance}%\n" +
            $" - ���� : {pot.potData.LegendChance}%\n" +
            $" - �̽��͸� : {pot.potData.MysteryChance}%";
    }
}
