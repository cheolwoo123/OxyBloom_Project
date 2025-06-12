using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private int _oxyzen;
    public PlantManager plantManager;
    public SoundManager soundManager;
    public UIManager uiManager;
    public Canvas notEnoughOxyzen;
    private PlayerData _playerData;


    private void Start()
    {
        if(uiManager != null)
            uiManager.Oxygen(Oxygen);
    }
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)
    {
        Oxygen = Oxygen + i;
        uiManager.Oxygen(Oxygen);
    }
    
    private IEnumerator NotEnoughOxyzen(int i)
    {
        if (i > _playerData.Oxygen)
        {
            notEnoughOxyzen.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            notEnoughOxyzen.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 키 입력 감지
        {
            StartCoroutine(NotEnoughOxyzen(10000));
        }
    }
}
