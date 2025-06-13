using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public SceneTransition sceneTransition;
    
    [Header("Buttons")]
    public Button titleButton;
    public Button exitButton;

    void Start()
    {
        titleButton.onClick.AddListener((() => TitleButton()));
        exitButton.onClick.AddListener((() => ExitButton()));
    }

    private void TitleButton()
    {
        Time.timeScale = 1f;
        sceneTransition.FadeAndLoadScene("Title");
    }
    
    private void ExitButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
