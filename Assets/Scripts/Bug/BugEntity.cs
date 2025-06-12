using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEntity : MonoBehaviour
{
    public BugScriptObject bugData;
    public int CurrentHP { get; private set; }

    private void OnEnable()
    {
        CurrentHP = bugData.maxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage; 
        if (CurrentHP < 0)
        {
            //Die();
        }
    }

    private void Die()
    {
        //������Ʈ Ǯ�� 
        //gameObject.SetActive(false);
    }
}
