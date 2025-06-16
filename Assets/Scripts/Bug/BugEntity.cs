using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEntity : MonoBehaviour
{
    public BugScriptObject bugData;

    private float currentHP;
    private float speed;

    public void Init(BugScriptObject data)
    {
        bugData = data;
        currentHP = bugData.maxHP;
        speed = bugData.speed;
    }

    public void SetHP(float hp)
    {
        currentHP = hp;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetHP()
    {
        return currentHP;
    }
    public bool IsDead => currentHP <= 0;
}
