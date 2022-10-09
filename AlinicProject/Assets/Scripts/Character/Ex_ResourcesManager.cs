using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : ���ҽ��Ŵ���. ���� ���ҽ��� �ҷ�����, ���ϴ� ���� ���ҽ� ����
 */
public class Ex_ResourcesManager : MonoBehaviour
{
    [HideInInspector]
    public static Ex_ResourcesManager instance;
    public List<GameObject> playableWeapons;    // ���⸮�ҽ� ����Ʈ
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
            Debug.Log(obj.name);
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
   
}
