public class PlayerStat
{
    public float plantMastery { get; private set; }
    public float attack { get; private set; }
    public int pmLevel { get; private set; }
    public int atkLevel { get; private set; }

    public void InitStat(int pm, int atk)
    {
        plantMastery = pm;
        attack = atk;
        PMLevelUp();
        ATKLevelUp();
    }

    public void PMLevelUp()
    {
        pmLevel++;
    }

    public void ATKLevelUp()
    {
        atkLevel++;
    }
}