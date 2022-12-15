using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scrips/ScriptableData/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float damage;   //���ݷ�
    public int maxAmmo;  // ź�� �ִ�ġ
    public int currentAmmo;
    public float skillCoolTime;
    public float currentCoolTime;
}
