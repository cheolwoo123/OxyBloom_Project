using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public SceneTransition sceneTransition;
    
    [Header("Buttons")]
    public Button titleButton;
    public Button exitButton;
    public Button SaveButton;
    public Button DeleteDataButton;

    void Start()
    {
        titleButton.onClick.AddListener((() => TitleButton()));
        exitButton.onClick.AddListener((() => ExitButton()));
        DeleteDataButton.onClick.AddListener(() => DeleteData());
        SaveButton.onClick.AddListener(() => GameManager.Instance.saveLoadManager.Save());  //저장 버튼 클릭시 저장 
    }

    private void TitleButton()  //타이틀로 나가기 버튼
    {
        Time.timeScale = 1f;
        sceneTransition.FadeAndLoadScene("Title");
    }
    
    private void ExitButton()  //나가기 버튼
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    
    private void DeleteData()
    {
        Time.timeScale = 1f;
        sceneTransition.FadeAndLoadScene("Title");
        GameManager.Instance.saveLoadManager.DeleteSaveData();
    }
}
