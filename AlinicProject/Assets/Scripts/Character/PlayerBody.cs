using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;

    private void Update()
    {
        UIManager.instance.UpdateHpState(maxHP, currentHP);
        if (currentHP < 0.01) GameManager.instance.Die();
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
