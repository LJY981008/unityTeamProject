using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scrips/ScriptableData/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float damage = 5;   //���ݷ�

    public int[] maxAmmo = { 30, 12, 12 };  // ź�� �ִ�ġ
    public int[] ammo = { 0, 0, 0 };        // ���� ź��
}
