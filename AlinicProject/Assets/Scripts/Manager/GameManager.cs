using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 게임매니저. 무기 생성, 무기 변경
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int gunIndex = 1;                        // 기본총 값
    public GameObject playerBox;

    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // 무기 목록
    private PlayerUpper playableWeapon;                      // 현재 사용 중인 무기

    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        SelectWeapon();    // 기본무기 권총
    }
    private void Update()
    {
        PlayerUtill.instance.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //좌클릭
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            playableWeapon.FireGun(gunIndex);
        }
        //우클릭
        else
        {
            playableWeapon.IdleGun();
        }
        
        //키 다운으로 무기 선택
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunIndex != 0)
        {
            gunIndex = 0;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gunIndex != 1)
        {
            //권총
            gunIndex = 1;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gunIndex != 2)
        {
            //샷건
            gunIndex = 2;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playableWeapon.ReloadGun(gunIndex);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate<GameObject>(ResourcesManager.instance.buffItem);
            obj.transform.position = PlayerUtill.GetRandomMapPos(playerBox.transform.position);
        }
    }
    public void SelectWeapon()
    {
        if (playableWeapon != null)
        {
            ObjectPoolManager.ReturnGun(playableWeapon);           // 이전 무기 오브젝트 및 컴포넌드 제거
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);        // 선택한 무기 생성
    }
    // 원하는 무기 씬에 생성
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = ResourcesManager.instance.GetPlayableWeapon(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = ObjectPoolManager.GetGun(weaponName);
        }
        else
        {
            Debug.Log("Create실패");
        }
    }
}
