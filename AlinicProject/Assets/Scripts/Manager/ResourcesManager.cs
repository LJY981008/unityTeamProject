using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : ���ҽ��Ŵ���. ���� ���ҽ��� �ҷ�����, ���ϴ� ���� ���ҽ� ����
 */
public class ResourcesManager : MonoBehaviour
{

    [HideInInspector] public static ResourcesManager instance;
    [HideInInspector] public List<GameObject> playableWeapons;    // ���⸮�ҽ� ����Ʈ
    [HideInInspector] public List<AudioClip> weaponAudioClips;    // ���� ���� ����Ʈ
    [HideInInspector] public List<GameObject> gunAmmos;           // �Ѿ� ����Ʈ ����Ʈ   ������ �̻��
    [HideInInspector] public GameObject ammo;                     // �Ѿ� ����Ʈ �ʿ�� gunAmmos ����Ʈ�� ��ü
    [HideInInspector] public List<Sprite> spriteBuffBody;         // ���� �̹� (���� ���� or źâ ����)
    [HideInInspector] public List<Sprite> spriteBuffIcon;         // ���� ������ (����Ǵ� ���� ����)
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
    // ���� ����
    public GameObject GetPlayableWeapon(string weaponName)
    {
        return playableWeapons.Find(o => o.name.Contains(weaponName));
    }
    // ���� ����
    public Sprite GetBuffBodySprite(string body)
    {
        return spriteBuffBody.Find(o => o.name.Equals(body));
    }
    // ���� ������
    public Sprite GetBuffIconSprite(string icon)
    {
        return spriteBuffIcon.Find(o => o.name.Contains(icon));
    }
    public GameObject GetBuffItem()
    {
        return buffItem;
    }
    // �� ���ҽ�
    public void LoadGuns()
    {
        GameObject[] objGuns = Resources.LoadAll<GameObject>(pathWeapons);
        foreach (var obj in objGuns)
        {
            playableWeapons.Add(obj);
        }
    }
    // �Ѿ� ����Ʈ ����Ʈ
    public void LoadAmmo()
    {
        ammo = Resources.Load<GameObject>(pathEffects);
    }
    // ���� ����Ʈ
    public void LoadBuffIcon()
    {
        Sprite[] objIcons = Resources.LoadAll<Sprite>(pathBuffIconSprite);
        foreach (var obj in objIcons)
        {
            spriteBuffIcon.Add(obj);
        }
    }
    // ���� ���� ����Ʈ
    public void LoadBuffBody()
    {
        Sprite[] objBodys = Resources.LoadAll<Sprite>(pathBuffBodySprite);
        foreach (var obj in objBodys)
        {
            spriteBuffBody.Add(obj);
            Debug.Log(obj.name);
        }
    }
    // ���� ������Ʈ
    public void LoadBuffItem()
    {
        buffItem = Resources.Load<GameObject>(pathBuffItem);
    }
    public void CharacterLoad()
    {
        if (playerChar == null)
            playerChar = new List<GameObject>();
        GameObject[] plChar = Resources.LoadAll<GameObject>(pathChar);
        // ĳ���� ���� �ȿ� �ִ� ��� ������ �޸𸮿� �ε�
        foreach (var obj in plChar) // ĳ���� ����Ʈ�� ����
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
