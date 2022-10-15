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
    private float ratio;    // (map UI �ػ� / plane�� scale) ��. ���� �̵����� �ʿ����� �̵����� ���߱� ���� 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ratio = 1.28f;
        boss = GameManager.instance.monster;
    }
    // �ʿ��� �̵��ϴ� �Լ�
    // �̵��Լ� �ȿ��� ���ŵǴ� �÷��̾��� ������ �޾ƿ��� 
    public void MovePlayerMap(Vector3 playerPos)
    {
        //�¿� x ���� y
        playerMovePos = subCoor(playerPos);
        map.localPosition = playerMovePos;

    }
    // ������ �� �÷��̾��� �ٶ󺸴� ���� ���ߴ� �Լ�
    // �÷��̾��� ������Ʈ Ŭ������ ������ �� rotation.eulerAngles �޾ƿ���
    public void SetPlayerMapFocus(Vector3 rotate)
    {
        playerForcus.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotate.z));
    }
    // �÷��̾ ȸ���� �� �ʿ����� ȸ�����ִ� �Լ�
    // ȸ�� �Լ��� ����� �� rotateY �� ����
    public void MovePlayerMapForcus(float dir)
    {
        Quaternion playerQuat = Quaternion.Euler(new Vector3(0.0f, 0.0f, dir));
        playerForcus.transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * 500f);

    }
    // ��ǥ ġȯ �Լ�
    // �̴ϸ��� ũ��� ���� ũ���� ������ �����ִ� �Լ�
    public Vector3 subCoor(Vector3 pos)
    {
        Vector3 tmp = map.position;
        tmp.x = pos.x * ratio;
        tmp.y = pos.z * ratio;
        return -tmp;
    }
    public Vector3 subCoorMob(Vector3 pos)
    {
        Vector3 tmp = monster.position;
        tmp.y = pos.y * ratio;
        tmp.x = pos.x * ratio;
        return -tmp;
    }

    // ���Ͱ� �̵��� �� ������ ��ġ * 1.28�� �Ͽ� ����ġ�� monster Image�� �̵��ϴ� �Լ� �ۼ�
    public void MoveMonsterMap()
    {
        /*monsterMovePos = subCoorMob(monster.position);
        monster.localPosition = monsterMovePos;*/
    }
}