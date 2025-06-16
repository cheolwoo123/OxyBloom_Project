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
        saveLoadManager.Load();
    }
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)
    {
        Oxygen = Oxygen + i;
        uiManager.Oxygen(Oxygen);
    }
    
    private IEnumerator NotEnoughOxyzen(int i)
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
            saveLoadManager.Save(saveLoadManager.GetSaveData());
            saveLoadManager.Load();
        }
        
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭
        {
            Debug.Log("asdasd");
            soundManager.ClickSound();
        }
    }

    public void SetSaveData()
    {
        saveLoadManager.SetSaveData(player.stat,_oxygen);
    }
}
