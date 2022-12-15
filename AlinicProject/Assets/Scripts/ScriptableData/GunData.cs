using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scrips/ScriptableData/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float damage;   //공격력
    public int maxAmmo;  // 탄약 최대치
    public int currentAmmo;
    public float skillCoolTime;
    public float currentCoolTime;
}
