using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;


    public void Awake()
    {
        stat = new PlayerStat(); 
        stat.InitStat(9, 9);
    }

    private void SavePlayerData()
    {
        GameManager.Instance.saveLoadManager.SetSaveData("PlayerStat", stat);  //데이터 저장
    }

    public void LoadPlayerData()
    {
        if (GameManager.Instance.GetSaveData().playerStat == null)
        {
            return;
        }

        stat = GameManager.Instance.GetSaveData().playerStat; 
    }
}
