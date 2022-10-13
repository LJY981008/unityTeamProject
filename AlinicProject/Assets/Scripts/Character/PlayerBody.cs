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
        UIManager.instance.UpdateHpState(maxHP, currentHP);
    }
        private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            UIManager.instance.imageBuffPanel.gameObject.SetActive(false);
        }
    }
}
