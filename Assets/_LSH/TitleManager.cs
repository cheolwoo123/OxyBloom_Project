
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public Button startButton;
    public SceneTransition sceneTransition;
    
    void Start()
    {
        startButton.onClick.AddListener((() => StartButton()));
    }

    private void StartButton()  //시작 버튼을 누르면 연출 후 씬 전환
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameObject.SetActive(true);
        }
        
        sceneTransition.FadeAndLoadScene("LSHTest2");
    }
}
