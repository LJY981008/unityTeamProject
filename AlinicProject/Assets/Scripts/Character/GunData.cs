using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scrips/ScriptableData/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float damage = 5;   //공격력

    public int[] maxAmmo = { 30, 12, 12 };  // 탄약 최대치
    public int[] ammo = { 0, 0, 0 };        // 현재 탄약
}
