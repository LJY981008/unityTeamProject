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
        CreateCharacter("Man_Full");
    }
    public void CreateCharacter(string _name)
    {
        // 캐릭터를 생성하는 코드를 작성
        GameObject tmpPlayerChar = CharResourcesManager.instance.GetCharacter(_name);
        if (tmpPlayerChar != null)
        {
            //playerChar = Instantiate(tmpPlayerChar, Vector3.zero, Quaternion.identity);
            // 캐릭터 게임오브젝트를 리스트에서 불러오는 코드
            playerChar = GameObject.Instantiate<GameObject>(tmpPlayerChar);
            player = playerChar.AddComponent<PlayerChar>();
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
