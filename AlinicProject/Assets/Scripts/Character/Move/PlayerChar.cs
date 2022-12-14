using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-22
 * 내용 : 캐릭터의 총괄 스크립트
 */
public class PlayerChar : MonoBehaviour
{
    public static PlayerChar instance;
    PlayerChar player;
    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // 달리기 키
    private KeyCode keyCodeJump = KeyCode.Space;    // 점프 키
    //
    private Movement movement; // 키보드 입력으로 플레이어 이동, 점프
    private Status status; // 이동속도 등의 플레이어 정보
    private float x, z;
    void Awake()
    {
        if (instance == null) instance = this;
        //커서를 보이지 않게 설정하고, 현제 위치에 고정시킨다.
        movement = instance.GetComponent<Movement>();
        status = instance.GetComponent<Status>();
    }
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        // 플레이어 이동
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        // 이동중 일 때 (걷기 or 뛰기)
        if ( x != 0 || z != 0)
        {
            bool isRun = false;
            // 옆이나 뒤로 이동할 대는 달릴 수 없다.
            if ( z > 0 ) isRun = Input.GetKey(keyCodeRun);
            instance.movement.MSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
        }
        instance.movement.Move(new Vector3(x, 0, z));
    }
    void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump))
        {
            instance.movement.Jump();
        }
    }
}
