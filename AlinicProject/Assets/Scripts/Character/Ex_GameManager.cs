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
    
    public Quaternion saveRotation;
    public Vector3 savePos;
    private string[] listWeapon = { "Rifle", "Pistol", "ShotGun" };    // ���� ���
    
    GameObject playableWeapon;                      // ���� ��� ���� ����
    void Awake()
    {
        if (instance == null) instance = this;
        saveRotation = Quaternion.identity;
        savePos = Vector3.zero;
        saveAmmo = new int[3];
    }
    private void Start()
    {
        SelectWeapon(gunIndex);    // �⺻���� ����
    }
    private void Update()
    {
        PlayerUpper.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
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
            saveRotation = PlayerUpper.instance.transform.rotation;
            savePos = PlayerUpper.instance.transform.position;
            saveAmmo[index] = PlayerUpper.instance.ammo[gunIndex];           // ���� ź�� ����
            Camera.main.transform.SetParent(null);
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
            playableWeapon = Instantiate(selectWeapon, savePos, saveRotation);
            playableWeapon.AddComponent<PlayerUpper>();
            CameraController.SettingCam(playableWeapon);
        }
        else
        {
            Debug.Log("Create����");
        }
    }
}
