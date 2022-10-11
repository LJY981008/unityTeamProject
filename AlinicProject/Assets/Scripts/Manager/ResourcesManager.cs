using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 리소스매니저. 무기 리소스들 불러오기, 원하는 무기 리소스 선택
 */
public class ResourcesManager : MonoBehaviour
{

    [HideInInspector] public static ResourcesManager instance;
    [HideInInspector] public List<GameObject> playableWeapons;    // 무기리소스 리스트
    [HideInInspector] public List<AudioClip> weaponAudioClips;    // 무기 사운드 리스트
    [HideInInspector] public List<GameObject> gunAmmos;           // 총알 이펙트 리스트   당장은 미사용
    [HideInInspector] public GameObject ammo;                     // 총알 이펙트 필요시 gunAmmos 리스트로 교체
    [HideInInspector] public List<Sprite> spriteBuffBody;         // 버프 이미 (무기 버프 or 탄창 버프)
    [HideInInspector] public List<Sprite> spriteBuffIcon;         // 버프 아이콘 (적용되는 버프 종류)
    [HideInInspector] public GameObject buffItem;
    [HideInInspector] public List<GameObject> playerChar;

    string pathWeapons = "Charactor/Weapons/Prefabs";
    string pathEffects = "Effect/Prefabs/Shoot_01";
    string pathBuffIconSprite = "Image/Buff Image/Icon";
    string pathBuffBodySprite = "Image/Buff Image/Body";
    string pathBuffItem = "Buff Item/Test Buff Item";
    string pathChar = "Charactor/Adventure_Character/Prefabs";
    private void Awake()
    {
        instance = this;
        LoadAmmo();
        CharacterLoad();
        LoadGuns();
        LoadBuffIcon();
        LoadBuffBody();
        LoadBuffItem();
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
    public GameObject GetBuffItem()
    {
        return buffItem;
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
    // 버프 오브젝트
    public void LoadBuffItem()
    {
        buffItem = Resources.Load<GameObject>(pathBuffItem);
    }
    public void CharacterLoad()
    {
        if (playerChar == null)
            playerChar = new List<GameObject>();
        GameObject[] plChar = Resources.LoadAll<GameObject>(pathChar);
        // 캐릭터 폴더 안에 있는 모든 에섯을 메모리에 로드
        foreach (var obj in plChar) // 캐릭터 리스트에 저장
        {
            playerChar.Add(obj);
        }
    }
    public GameObject GetChar(string _name)
    {
        foreach (var charObj in playerChar)
        {
            if (charObj.name.Equals(_name))
                return charObj;
        }
        return null;
    }
    public void aa() { }
}
