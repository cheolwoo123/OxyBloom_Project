using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private int _oxyzen;
    //public PlantManager  plantManager;
    public SoundManager soundManager;
    public UIManager uiManager;
    public Canvas notEnoughOxyzen;


    private void Start()
    {
        uiManager.Oxyzen(Oxyzen);
    }
    
    public int Oxyzen{get{return _oxyzen;} private set{_oxyzen = value;}}

    public void SetOxyzen(int i)
    {
        Oxyzen = Oxyzen + i;
        uiManager.Oxyzen(Oxyzen);
    }
    
    private IEnumerator NotEnoughOxyzen()
    {
        notEnoughOxyzen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughOxyzen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 키 입력 감지
        {
            StartCoroutine(NotEnoughOxyzen());
        }
    }
}
