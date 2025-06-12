




public class PlayerData
{
    //public PlantManager  plantManager;
    private int _oxygen;
    
    public int Oxygen{get{return _oxygen;} private set{_oxygen = value;}}

    public void SetOxygen(int i)
    {
        Oxygen = Oxygen + i;
        GameManager.Instance.uiManager.Oxygen(Oxygen);
    }
}