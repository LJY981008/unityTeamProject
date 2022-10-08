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
        if (instance == null) instance = this;

        // 리소스로 저장된 무기들을 불러와서 리스트에 저장 
        //ammo = Resources.Load<GameObject>("Charactor/Weapons/Prefabs/Ammo");
        GameObject[] objGuns = Resources.LoadAll<GameObject>("Charactor/Weapons/Prefabs");
        foreach (var obj in objGuns)
        {
            playableWeapons.Add(obj);
        }
        LoadAmmo();

    }

    // 불러온 무기 리스트에서 원하는 무기 선택
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
