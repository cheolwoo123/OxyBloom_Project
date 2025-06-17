using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;
    
    public void Start()
    {
        LoadPlayerData();
    }

    private void SavePlayerData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData("PlayerStat", stat);  //데이터 저장
    }

    public void LoadPlayerData()
    {
        if (GameManager.Instance.GetSaveData().playerStat == null)
        {
            stat.InitStat(9, 9);  //초기화
            return;
        }

        stat = GameManager.Instance.GetSaveData().playerStat; 
    }
}
