using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private int _oxyzen;
    //public PlantManager plantManager;
    public SoundManager soundManager;
    public UIManager uiManager;


    public int Oxyzen{get{return _oxyzen;} private set{_oxyzen = value;}}






}
