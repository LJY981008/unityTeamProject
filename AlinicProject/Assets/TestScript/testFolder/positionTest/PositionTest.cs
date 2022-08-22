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
        // 현재 좌표가 랜덤 좌표와 같을 때
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
        // n초마다 랜덤 좌표 지정 후 이동(시간초 초기화)
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
