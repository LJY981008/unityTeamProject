using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Debug.Log("∏ÛΩ∫≈Õ");
    }

   
}
