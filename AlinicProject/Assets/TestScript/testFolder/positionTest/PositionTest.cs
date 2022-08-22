using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour
{
    Vector3 randomTarget;
    float moveSpeed;
    float latelyTime;

    // Start is called before the first frame update
    void Start()
    {
        latelyTime = Time.time;
        moveSpeed = 3.5f;
        randomTarget = Random.insideUnitSphere * 15.0f;
        randomTarget.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveToRandomPos();
    }

    void moveToRandomPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomTarget, Time.deltaTime * moveSpeed);
        // ���� ��ǥ�� ���� ��ǥ�� ���� ��
        if (transform.position == randomTarget)
        {
            randomTarget = Random.insideUnitSphere * 15.0f;
            randomTarget.y = 0f;
            Debug.Log(randomTarget);
        }
    }

    void moveToRandomPosBySec()
    {
        Debug.Log(Time.time - latelyTime);
        transform.position = Vector3.MoveTowards(transform.position, randomTarget, Time.deltaTime * moveSpeed);
        // n�ʸ��� ���� ��ǥ ���� �� �̵�(�ð��� �ʱ�ȭ)
        if (Time.time - latelyTime >= 1.0f)
        {
            randomTarget = Random.insideUnitSphere * 15.0f;
            randomTarget.y = 0f;
            initTime();
            Debug.Log(randomTarget);
        }
    }

    void initTime()
    {
        latelyTime = Time.time;
    }
}
