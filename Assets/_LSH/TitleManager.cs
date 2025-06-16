
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

    private void StartButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameObject.SetActive(true);
        }
        
        sceneTransition.FadeAndLoadScene("LSHTest2");
    }
}
