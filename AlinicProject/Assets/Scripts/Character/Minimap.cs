using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public static Minimap instance;

    public Image playerForcus;
    public RectTransform monster;
    public RectTransform map;
    private GameObject boss;
    private Vector3 playerMovePos;
    private Vector3 monsterMovePos;
    private float ratio;    // (map UI 해상도 / plane의 scale) 몫. 실제 이동량과 맵에서의 이동량을 맞추기 위함 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ratio = 1.28f;
        boss = GameManager.instance.monster;
    }
    // 맵에서 이동하는 함수
    // 이동함수 안에서 갱신되는 플레이어의 포지션 받아오기 
    public void MovePlayerMap(Vector3 playerPos)
    {
        //좌우 x 상하 y
        playerMovePos = subCoorPlayer(playerPos);
        map.localPosition = playerMovePos;

    }
    // 시작할 때 플레이어의 바라보는 방향 맞추는 함수
    // 플레이어의 오브젝트 클래스가 생성될 때 rotation.eulerAngles 받아오기
    public void SetPlayerMapFocus(Vector3 rotate)
    {
        playerForcus.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotate.z));
    }
    // 플레이어가 회전할 때 맵에서도 회전해주는 함수
    // 회전 함수를 사용할 때 rotateY 값 적용
    public void MovePlayerMapForcus(float dir)
    {
        Quaternion playerQuat = Quaternion.Euler(new Vector3(0.0f, 0.0f, dir));
        playerForcus.transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * 500f);

    }
    // 몬스터 위치 표시함수
    public void MoveMonsterMap()
    {
        monsterMovePos = subCoorMonster(boss.transform.position);
        monster.localPosition = monsterMovePos;
    }
    // 좌표 치환 함수
    // 미니맵의 크기와 실제 크기의 비율을 맞춰주는 함수
    public Vector3 subCoorPlayer(Vector3 pos)
    {
        Vector3 tmp = map.position;
        tmp.x = pos.x * ratio;
        tmp.y = pos.z * ratio;
        Debug.Log(-tmp);
        return -tmp;
    }
    public Vector3 subCoorMonster(Vector3 pos)
    {
        Vector3 tmp = monster.position;
        tmp.x = pos.y * ratio;
        tmp.y = -pos.x * ratio;
        //Debug.Log(-tmp);
        return -tmp;
    }
}