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
        if (instance == null) instance = this;

        // ���ҽ��� ����� ������� �ҷ��ͼ� ����Ʈ�� ���� 
        //ammo = Resources.Load<GameObject>("Charactor/Weapons/Prefabs/Ammo");
        GameObject[] objGuns = Resources.LoadAll<GameObject>("Charactor/Weapons/Prefabs");
        foreach (var obj in objGuns)
        {
            playableWeapons.Add(obj);
        }
        LoadAmmo();

    }

    // �ҷ��� ���� ����Ʈ���� ���ϴ� ���� ����
    public GameObject GetPlayableCharactor(string weaponName)
    {
        foreach (var objWeapon in playableWeapons) { 
            if (objWeapon.name.Equals(weaponName)) return objWeapon; 
        }
        return null;
    }

    public void LoadAmmo()
    {
        //GameObject[] objAmmo = Resources.LoadAll<GameObject>("Effect/Prefabs/");
        ammo = Resources.Load<GameObject>("Effect/Prefabs/Shoot_01");
        /*foreach (var obj in objAmmo)
        {
            gunAmmos.Add(obj);
        }*/
    }

    public List<GameObject> GetAmmo()
    {
        return gunAmmos;
    }
    
   
}
