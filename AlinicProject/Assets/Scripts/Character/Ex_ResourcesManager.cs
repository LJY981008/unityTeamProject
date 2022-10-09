using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 리소스매니저. 무기 리소스들 불러오기, 원하는 무기 리소스 선택
 */
public class Ex_ResourcesManager : MonoBehaviour
{
    [HideInInspector]
    public static Ex_ResourcesManager instance;
    public List<GameObject> playableWeapons;    // 무기리소스 리스트
    public List<AudioClip> pistolAudioClip;
    public List<GameObject> gunAmmos;
    public GameObject ammo;
    public List<Sprite> spriteBuffBody;
    public List<Sprite> spriteBuffIcon;

    string pathWeapons = "Charactor/Weapons/Prefabs";
    string pathEffects = "Effect/Prefabs/Shoot_01";
    string pathBuffIconSprite = "Image/Buff Image/Icon";
    string pathBuffBodySprite = "Image/Buff Image/Body";
    private void Awake()
    {
        instance = this;
        LoadAmmo();
        LoadGuns();
        LoadBuffIcon();
        LoadBuffBody();
    }
    // 무기 선택
    public GameObject GetPlayableWeapon(string weaponName)
    {
        return playableWeapons.Find(o => o.name.Contains(weaponName));
    }
    // 버프 종류
    public Sprite GetBuffBodySprite(string body)
    {
        return spriteBuffBody.Find(o => o.name.Equals(body));
    }
    // 버프 아이콘
    public Sprite GetBuffIconSprite(string icon)
    {
        return spriteBuffIcon.Find(o => o.name.Contains(icon));
    }
    // 총 리소스
    public void LoadGuns()
    {
        GameObject[] objGuns = Resources.LoadAll<GameObject>(pathWeapons);
        foreach (var obj in objGuns)
        {
            playableWeapons.Add(obj);
        }
    }
    // 총알 이펙트 리스트
    public void LoadAmmo()
    {
        ammo = Resources.Load<GameObject>(pathEffects);
    }
    // 버프 리스트
    public void LoadBuffIcon()
    {
        Sprite[] objIcons = Resources.LoadAll<Sprite>(pathBuffIconSprite);
        foreach (var obj in objIcons)
        {
            spriteBuffIcon.Add(obj);
            Debug.Log(obj.name);
        }
    }
    // 버프 종류 리스트
    public void LoadBuffBody()
    {
        Sprite[] objBodys = Resources.LoadAll<Sprite>(pathBuffBodySprite);
        foreach (var obj in objBodys)
        {
            spriteBuffBody.Add(obj);
            Debug.Log(obj.name);
        }
    }
   
}
