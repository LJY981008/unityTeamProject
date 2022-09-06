using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-18
 * 내용 : 캐릭터의 리소스매니저
 */
public class CharResourcesManager : MonoBehaviour
{
    public static CharResourcesManager instance; // 싱글톤 선언
    List<GameObject> playerChar; // 캐릭터 리스트 변수 선언
    void Awake()
    {
        if (instance == null) instance = this; // 싱글톤의 null 검사
        CharacterLoad();
    }
    public void CharacterLoad()
    {
        if (playerChar == null)
            playerChar = new List<GameObject>();
        GameObject[] plChar = Resources.LoadAll<GameObject>("Charactor/Adventure_Character/Prefabs");
        // 캐릭터 폴더 안에 있는 모든 에섯을 메모리에 로드
        foreach(var obj in plChar) // 캐릭터 리스트에 저장
        {
            playerChar.Add(obj);
        }
    }
    public GameObject GetCharacter(string _name)
    {
        foreach (var charObj in playerChar)
        {
            if (charObj.name.Equals(_name))
                return charObj;
        }
        return null;
    }
}
