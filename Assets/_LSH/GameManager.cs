using System.Collections;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private int _oxygen;
    public PlantManager plantManager;
    public SoundManager soundManager;
    public UIManager uiManager;
    public Canvas notEnoughOxyzen;
    public Player player;
    public SaveLoadManager saveLoadManager;
    
    

    private void Start()
    {
        if(uiManager != null)
            uiManager.Oxygen(Oxygen);
        
        SaveData saveData = saveLoadManager.Load();
        
        if (saveData != null)
        {
            Oxygen = saveData._oxygen;
            uiManager.Oxygen(Oxygen);
            player.stat = saveData._playerStat;
        }

    }
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)  //산소 값, UI 초기화, 
    {
        Oxygen = Oxygen + i;
        uiManager.Oxygen(Oxygen);
        saveLoadManager.SetSaveData(player.stat,Oxygen);
    }
    
    private IEnumerator NotEnoughOxyzen(int i)  //산소 부족 알림 띄우기
    {
        if (i > Oxygen)
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
            //StartCoroutine(NotEnoughOxyzen(10000));
            saveLoadManager.SetSaveData(player.stat,10000);
            saveLoadManager.Load();
        }
        
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭
        {
            soundManager.ClickSound();
        }
    }
}
