using UnityEngine;

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
