using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 100;

    private void Update()
    {
        UIManager.instance.UpdateHpState(maxHP, currentHP);
    }
    public void OnDamage(float damege)
    {
        currentHP -= damege;
        
    }
        private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            UIManager.instance.imageBuffPanel.gameObject.SetActive(false);
        }
    }
}
