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
    
    private SaveData saveData;

    private void OnEnable()
    {
        saveData = saveLoadManager.Load();
        Debug.Log("saveData = " + saveData.oxygen);
        
        if (saveData != null)
        {
            Oxygen = saveData.oxygen;
            
            Debug.Log("Oxygen = "+Oxygen);
            if(uiManager != null)
                uiManager.Oxygen(Oxygen);
        }
    }
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)  //산소 값, UI 초기화, 
    {
        Oxygen = Oxygen + i;
        uiManager.Oxygen(Oxygen);
        saveLoadManager.SetSaveData<int>("Oxygen", Oxygen);
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
            saveLoadManager.SetSaveData<int>("Oxygen", 10000);
            saveLoadManager.Load();
            uiManager.Oxygen(Oxygen);
        }
        
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭
        {
            soundManager.ClickSound();
        }
    }
    
    public SaveData GetSaveData()
    {
        return saveData;
    }
}
