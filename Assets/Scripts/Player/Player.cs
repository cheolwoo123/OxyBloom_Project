using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;

    public void Start()
    {
        stat = new PlayerStat();
        LoadPlayerData();
    }

    public void PMLevelUp()
    {
        stat.PMLevelUp(); 
        Debug.Log($"식물 숙련도 레벨업! 현재 숙련도 레벨: {stat.pmLevel}");
    }

    public void ATKLevelUp()
    {
        stat.ATKLevelUp();
        Debug.Log($"공격력 레벨업! 현재 공격력 레벨: {stat.atkLevel}");   
    }

    public void LoadPlayerData()
    {
        if (GameManager.Instance.GetSaveData().playerStat == null)
        {
            return;
        }

        stat = GameManager.Instance.GetSaveData().playerStat; 

        Debug.Log($"플레이어 데이터 로드 완료: PM Level: {stat.pmLevel}, ATK Level: {stat.atkLevel}");
    }
}
