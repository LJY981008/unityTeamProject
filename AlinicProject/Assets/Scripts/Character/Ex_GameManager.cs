using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : ���ӸŴ���. ���� ����, ���� ����
 */
public class Ex_GameManager : MonoBehaviour
{
    public static Ex_GameManager instance;

    private string[] listWeapon = { "Rifle", "Pistol" };    // ���� ���

    PlayerUpper playerUpper;    // ��ü ��ũ��Ʈ 
    GameObject playableWeapon;  // ���� ��� ���� ����

    void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        CreatePlayableWeapon(listWeapon[1]);   // �⺻ ���⸦ ��������
    }
    private void Update()
    {
        //Ű �ٿ����� ���� ����
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("1�� ���� : " + listWeapon[0]);
            Destroy(playableWeapon);
            //�ֹ��� ���� �ۼ�
        }
        else if (Input.GetKeyDown("2"))
        {
            Debug.Log("2������ : " + listWeapon[1]);
            Destroy(playableWeapon);
            CreatePlayableWeapon(listWeapon[1]);
        }
    }

    // ���ϴ� ���� ���� ����
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = Ex_ResourcesManager.instance.GetPlayableCharactor(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = Instantiate(selectWeapon, Vector3.zero, Quaternion.identity);
            playerUpper = playableWeapon.AddComponent<PlayerUpper>();
        }
        else
        {
            Debug.Log("Create����");
        }
    }
}
