public class PlayerStat
{
    private int plantMastery;
    private int attack;

    public void IncreasedPM(int amount)
    {
        plantMastery += amount;
    }

    public void IncreasedATK(int amount)
    {
        attack += amount;
    }
}