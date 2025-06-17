using UnityEngine;

//PlayerStat
// stat = GameManager.Instance.GetSaveData().playerStat;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData<PlayerStat>("PlayerStat", stat);  //데이터 저장

public class Player : MonoBehaviour
{
    public PlayerStat stat;

    public void Awake()
    {
        stat = new PlayerStat();
        {
            stat.InitStat(9, 9);
        }
    }
}
