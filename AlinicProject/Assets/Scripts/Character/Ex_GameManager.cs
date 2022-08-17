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

    private string[] listWeapon = { "Rifle", "Pistol" };    // 무기 목록

    PlayerUpper playerUpper;    // 상체 스크립트 
    GameObject playableWeapon;  // 현재 사용 중인 무기

    void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        CreatePlayableWeapon(listWeapon[1]);   // 기본 무기를 권총으로
    }
    private void Update()
    {
        //키 다운으로 무기 선택
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("1번 무기 : " + listWeapon[0]);
            Destroy(playableWeapon);
            //주무기 고르기 작성
        }
        else if (Input.GetKeyDown("2"))
        {
            Debug.Log("2번무기 : " + listWeapon[1]);
            Destroy(playableWeapon);
            CreatePlayableWeapon(listWeapon[1]);
        }
    }

    // 원하는 무기 씬에 생성
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
            Debug.Log("Create실패");
        }
    }
}
