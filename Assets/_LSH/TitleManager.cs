using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button startButton;

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
        
        SceneManager.LoadScene("Gamemanager");
    }
}
