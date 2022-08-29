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
    public int[] saveAmmo;                          // ����ϰ� ����� ź 
    public int gunIndex = 1;                        // �⺻�� ��

    private string[] listWeapon = { "Rifle", "Pistol", "ShotGun" };    // ���� ���
    
    GameObject playableWeapon;                      // ���� ��� ���� ����
    void Awake()
    {
        if (instance == null) instance = this;
        saveAmmo = new int[3];
        Debug.Log("����");
    }
    private void Start()
    {
        SelectWeapon(gunIndex);    // �⺻���� ����
    }
    private void Update()
    {
        //��Ŭ��
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && PlayerUpper.instance.ammo[gunIndex] > 0)
        {
            PlayerUpper.instance.FireGun(gunIndex);
        }
        //��Ŭ��
        else
        {
            PlayerUpper.instance.IdleGun();
        }
        
        //Ű �ٿ����� ���� ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //������
            gunIndex = 0;
            SelectWeapon(gunIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //����
            gunIndex = 1;
            SelectWeapon(gunIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //����
            gunIndex = 2;
            SelectWeapon(gunIndex);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerUpper.instance.ReloadGun(gunIndex);
        }


    }


    public void SelectWeapon(int index)
    {
        
        if (playableWeapon != null)
        {
            saveAmmo[index] = PlayerUpper.instance.ammo[gunIndex];           // ���� ź�� ����
            DestroyImmediate(playableWeapon);           // ���� ���� ������Ʈ �� �����͵� ����
        }
        CreatePlayableWeapon(listWeapon[index]);        // ������ ���� ����
        PlayerUpper.instance.ammo[gunIndex] = saveAmmo[index];           // ���� ź�� ����
    }


    // ���ϴ� ���� ���� ����
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = Ex_ResourcesManager.instance.GetPlayableCharactor(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = Instantiate(selectWeapon, Vector3.zero, Quaternion.identity);
            playableWeapon.AddComponent<PlayerUpper>();
        }
        else
        {
            Debug.Log("Create����");
        }
    }
}
