using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;

    public void Awake()
    {
        stat = new PlayerStat();
        {
            stat.InitStat(9, 9);
            Debug.Log($"PM Level: {stat.pmLevel}, Plant Mastery: {stat.plantMastery}");
            Debug.Log($"ATK Level: {stat.atkLevel}, Attack: {stat.attack}");
        }
    }
}
