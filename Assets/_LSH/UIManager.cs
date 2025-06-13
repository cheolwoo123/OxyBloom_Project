using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button optionButton;
    
    [Header("Texts")]
    public TextMeshProUGUI oxygenText;

    
    [Header("Canvas")]
    public Canvas optionCanvas;
    
    private void OnEnable()
    {
        optionButton.onClick.AddListener((() => UICanvas_OnClick()));
    }

    private void UICanvas_OnClick()
    {
        optionCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Oxygen(int oxygen)
    {
        oxygenText.text = "- " + oxygen;
    }
}
