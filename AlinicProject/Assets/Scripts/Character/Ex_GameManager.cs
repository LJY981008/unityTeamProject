using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 게임매니저. 무기 생성, 무기 변경
 */
public class Ex_GameManager : MonoBehaviour
{
    public static Ex_GameManager instance;
    public int[] saveAmmo;                          // 사용하고 저장된 탄 
    public int gunIndex = 1;                        // 기본총 값
    
    public Quaternion saveRotation;
    public Vector3 savePos;
    private string[] listWeapon = { "Rifle", "Pistol", "ShotGun" };    // 무기 목록
    
    GameObject playableWeapon;                      // 현재 사용 중인 무기
    void Awake()
    {
        if (instance == null) instance = this;
        saveRotation = Quaternion.identity;
        savePos = Vector3.zero;
        saveAmmo = new int[3];
    }
    private void Start()
    {
        SelectWeapon(gunIndex);    // 기본무기 권총
    }
    private void Update()
    {
        PlayerUpper.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //좌클릭
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && PlayerUpper.instance.ammo[gunIndex] > 0)
        {
            PlayerUpper.instance.FireGun(gunIndex);
        }
        //우클릭
        else
        {
            PlayerUpper.instance.IdleGun();
        }
        
        //키 다운으로 무기 선택
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //라이플
            gunIndex = 0;
            
            SelectWeapon(gunIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //권총
            gunIndex = 1;
            SelectWeapon(gunIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //샷건
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
            saveAmmo[index] = PlayerUpper.instance.ammo[gunIndex];           // 남은 탄약 저장
            Camera.main.transform.SetParent(null);
            DestroyImmediate(playableWeapon);           // 이전 무기 오브젝트 및 컴포넌드 제거
        }
        CreatePlayableWeapon(listWeapon[index]);        // 선택한 무기 생성
        PlayerUpper.instance.ammo[gunIndex] = saveAmmo[index];           // 이전 탄약 유지
    }


    // 원하는 무기 씬에 생성
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
            Debug.Log("Create실패");
        }
    }
}
