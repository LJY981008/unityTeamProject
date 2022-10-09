using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : ���ҽ��Ŵ���. ���� ���ҽ��� �ҷ�����, ���ϴ� ���� ���ҽ� ����
 */
public class Ex_ResourcesManager : MonoBehaviour
{
    public static Ex_ResourcesManager instance;
    public List<GameObject> playableWeapons;    // ���⸮�ҽ� ����Ʈ
    public List<AudioClip> pistolAudioClip;
    public static List<GameObject> gunAmmos;
    public static GameObject ammo;
    void Awake()
    {
        instance = this;
        LoadAmmo();
        LoadGuns();
    }

    // �ҷ��� ���� ����Ʈ���� ���ϴ� ���� ����
    public GameObject GetPlayableCharactor(string weaponName)
    {
        foreach (var objWeapon in playableWeapons) { 
            if (objWeapon.name.Equals(weaponName)) return objWeapon; 
        }
        return null;
    }

    public void LoadGuns()
    {
        GameObject[] objGuns = Resources.LoadAll<GameObject>("Charactor/Weapons/Prefabs");
        foreach (var obj in objGuns)
        {
            playableWeapons.Add(obj);
        }
    }
    public void LoadAmmo()
    {
        ammo = Resources.Load<GameObject>("Effect/Prefabs/Shoot_01");
    }
    
   
}
