using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public static Minimap instance;

    public Image playerForcus;
    public Image monster;
    public RectTransform map;
    private Vector3 movePos;
    private float ratio;    // (map UI 해상도 / plane의 scale) 몫. 실제 이동량과 맵에서의 이동량을 맞추기 위함 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ratio = 1.28f;
    }
    // 맵에서 이동하는 함수
    // 이동함수 안에서 갱신되는 플레이어의 포지션 받아오기 
    public void MovePlayerMap(Vector3 playerPos)
    {
        //좌우 x 상하 y
        movePos = subCoor(playerPos);
        map.localPosition = movePos;
        Debug.Log(movePos + " : " + map.localPosition);

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
    // 좌표 치환 함수
    // 미니맵의 크기와 실제 크기의 비율을 맞춰주는 함수
    public Vector3 subCoor(Vector3 pos)
    {
        Vector3 tmp = map.position;
        tmp.x = pos.x * ratio;
        tmp.y = pos.z * ratio;
        return -tmp;
    }

    // 몬스터가 이동할 때 몬스터의 위치 * 1.28을 하여 그위치로 monster Image를 이동하는 함수 작성

}