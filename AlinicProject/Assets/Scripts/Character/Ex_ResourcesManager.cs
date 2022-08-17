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

    void Awake()
    {
        if (instance == null) instance = this;

        // ���ҽ��� ����� ������� �ҷ��ͼ� ����Ʈ�� ���� 
        GameObject[] characters = Resources.LoadAll<GameObject>("Charactor/Weapons/Prefabs");
        foreach (var obj in characters)
        {
            playableWeapons.Add(obj);
        }
    }

    // �ҷ��� ���� ����Ʈ���� ���ϴ� ���� ����
    public GameObject GetPlayableCharactor(string weaponName)
    {
        foreach (var objWeapon in playableWeapons) { 
            if (objWeapon.name.Equals(weaponName)) return objWeapon; 
        }
        return null;
    }
}
