using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    public int pmLevel = 1;
    public int atkLevel = 1;
  
    public void PMLevelUp()
    {
        pmLevel++;
    }

    public void ATKLevelUp()
    {
        atkLevel++;
    }

    public bool UpgradePot(PotInstance pot)
    {

        // 강화 비용  초기 100
        int cost = Mathf.FloorToInt(100 * Mathf.Pow(pot.potData.upgradePotExpense, pot.level - 1));
       

        if (GameManager.Instance.Oxygen < cost || pot.level >= pot.potData.maxLevel)
        {           
            GameManager.Instance.StartCoroutine("NotEnoughOxyzen", cost);
            return false;
        }

        GameManager.Instance.SetOxygen(-cost);
        pot.level++;
        return true;
    }
}