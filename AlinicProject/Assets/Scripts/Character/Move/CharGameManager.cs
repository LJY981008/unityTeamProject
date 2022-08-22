using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-18
 * 내용 : 캐릭터의 게임매니저
 */
public class CharGameManager : MonoBehaviour
{
    public static CharGameManager instance;
    PlayerChar player;
    GameObject playerChar;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        CreateCharacter("Man");
    }
    public void CreateCharacter(string _name)
    {
        GameObject tmpPlayerChar = CharResourcesManager.instance.GetCharacter(_name);
        if (tmpPlayerChar != null)
        {
            //playerChar = Instantiate(tmpPlayerChar, Vector3.zero, Quaternion.identity);
            playerChar = GameObject.Instantiate<GameObject>(tmpPlayerChar);
            player = playerChar.AddComponent<PlayerChar>();
            // 카메라에게 무엇이 플에이어의 스크립트인지 알려주는 코드
            // PlayerCamera.instance.PLAYER = player;
            // 플레이어가 생성될때 플레이어의 위치를 얻는 코드
            //PlayerCamera.instance.PLAYEROLDPOS = player.transform.position;
            // 카메라의 위치를 알려주는 코드
        }
        else
        {
            Debug.Log("생성 실패");
        }
    }
    void Update()
    {
    
    }
}
