using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 리소스매니저. 무기 리소스들 불러오기, 원하는 무기 리소스 선택
 */
public class Ex_ResourcesManager : MonoBehaviour
{
    public static Ex_ResourcesManager instance;
    public List<GameObject> playableWeapons;    // 무기리소스 리스트
    public List<AudioClip> pistolAudioClip;
    public static List<GameObject> gunAmmos;
    public static GameObject ammo;
    void Awake()
    {
        instance = this;
        LoadAmmo();
        LoadGuns();
    }

    // 불러온 무기 리스트에서 원하는 무기 선택
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
