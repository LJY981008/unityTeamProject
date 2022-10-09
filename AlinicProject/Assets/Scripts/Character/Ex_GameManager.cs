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
    public int gunIndex = 1;                        // �⺻�� ��
    public Quaternion saveRotation;
    public Vector3 savePos;
    public GameObject playerBox;
    public GameObject sceneBox;

    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // ���� ���
    private PlayerUpper playableWeapon;                      // ���� ��� ���� ����

    void Awake()
    {
        instance = this;
        saveRotation = Quaternion.identity;
        savePos = Vector3.zero;
        
    }
    private void Start()
    {
        SelectWeapon();    // �⺻���� ����
    }
    private void Update()
    {
        PlayerUtill.instance.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //��Ŭ��
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            playableWeapon.FireGun(gunIndex);
        }
        //��Ŭ��
        else
        {
            playableWeapon.IdleGun();
        }
        
        //Ű �ٿ����� ���� ����
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunIndex != 0)
        {
            gunIndex = 0;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gunIndex != 1)
        {
            //����
            gunIndex = 1;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gunIndex != 2)
        {
            //����
            gunIndex = 2;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playableWeapon.ReloadGun(gunIndex);
        }

    }
    public void SelectWeapon()
    {
        if (playableWeapon != null)
        {
            saveRotation = playableWeapon.transform.rotation;
            savePos = playableWeapon.transform.position;
            ObjectPoolManager.ReturnGun(playableWeapon);           // ���� ���� ������Ʈ �� �����͵� ����
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);        // ������ ���� ����
    }
    // ���ϴ� ���� ���� ����
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = Ex_ResourcesManager.instance.GetPlayableCharactor(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = ObjectPoolManager.GetGun(weaponName, savePos);
        }
        else
        {
            Debug.Log("Create����");
        }
    }
}
