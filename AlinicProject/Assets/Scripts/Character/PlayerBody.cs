using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 100;
    
    public void OnDamage(float damege)
    {
        currentHP -= damege;
        UIManager.UpdateHpState(maxHP, currentHP);
    }
}
