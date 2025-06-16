using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button optionButton;
    public Button collectionButton;
    public Button upgradeButton;
    public Button plantButton;
    public Button sheifButton;
    public Button ExitCollectionButton;

    [Header("Texts")]
    public TextMeshProUGUI oxygenText;
    public TextMeshProUGUI surviveDaysText;
    public TextMeshProUGUI roundTimeText;

    [Header("Canvas")]
    public Canvas optionCanvas;

    [Header("GameObject")]
    public GameObject colletionUI;
    public GameObject upgradeUI;
    
    private void OnEnable()
    {
        optionButton.onClick.AddListener((() => UICanvas_OnClick()));
        collectionButton.onClick.AddListener(() => DisplayCollectionUI());
        upgradeButton.onClick.AddListener(() => DisplayUpgradeUI());
        ExitCollectionButton.onClick.AddListener(() => DisplayCollectionUI());
    }

    private void UICanvas_OnClick()
    {
        optionCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void DisplayCollectionUI()
    {
        if (colletionUI.activeSelf)
        {
            colletionUI.SetActive(false);
            return;
        }

        colletionUI.SetActive(true);
    }

    private void DisplayUpgradeUI()
    {
        if (upgradeUI.activeSelf)
        {
            upgradeUI.SetActive(false);
            return;
        }

        upgradeUI.SetActive(true);
    }

    public void DisplayDays(int day)
    {
        surviveDaysText.text = day.ToString() + " Days";
    }

    public void DisplayWaveTime(float time)
    {
        roundTimeText.text = "웨이브 시간\n" + time.ToString("F2");
    }

    public void Oxygen(int oxygen)
    {
        oxygenText.text = "- " + oxygen;
    }

    public void DisplaySheifButton()
    {
        sheifButton.gameObject.SetActive(!sheifButton.gameObject.activeSelf);
    }


    public void DisplayPlantButton()
    {
        plantButton.gameObject.SetActive(!plantButton.gameObject.activeSelf);
    }
}

