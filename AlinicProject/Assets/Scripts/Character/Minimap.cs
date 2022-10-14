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
    private float ratio;    // (map UI �ػ� / plane�� scale) ��. ���� �̵����� �ʿ����� �̵����� ���߱� ���� 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ratio = 1.28f;
    }
    // �ʿ��� �̵��ϴ� �Լ�
    // �̵��Լ� �ȿ��� ���ŵǴ� �÷��̾��� ������ �޾ƿ��� 
    public void MovePlayerMap(Vector3 playerPos)
    {
        //�¿� x ���� y
        movePos = subCoor(playerPos);
        map.localPosition = movePos;
        Debug.Log(movePos + " : " + map.localPosition);

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

    // ���Ͱ� �̵��� �� ������ ��ġ * 1.28�� �Ͽ� ����ġ�� monster Image�� �̵��ϴ� �Լ� �ۼ�

}