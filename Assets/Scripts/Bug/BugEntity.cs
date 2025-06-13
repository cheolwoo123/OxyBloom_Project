using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEntity : MonoBehaviour
{
    public BugScriptObject bugData;
    public int CurrentHP { get; private set; }

    public void Init(BugScriptObject data)
    {
        bugData = data;
        CurrentHP = bugData.maxHP;
    }

    public void SetHP(int hp)
    {
        CurrentHP = hp;
    }

    public bool IsDead => CurrentHP <= 0;
}
