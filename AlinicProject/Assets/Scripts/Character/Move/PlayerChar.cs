using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-22
 * 내용 : 캐릭터의 이동구현 및 카메라 이동 구현
 */
public class PlayerChar : MonoBehaviour
{
    public static PlayerChar instance;
     private RotateToMouse_ rotateToMouse; // 마우스 이동으로 카메라 회전
    private float MouseX, MouseY, keyX, KeyZ;
    private MovementCharaterController movement; // 키보드 입력으로 플레이어 이동, 점프
    PlayerChar player;
    void Awake()
    {
        if (instance == null) instance = this;
        //커서를 보이지 않게 설정하고, 현제 위치에 고정시킨다.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rotateToMouse = GetComponent<RotateToMouse_>();
        movement = GetComponent<MovementCharaterController>();
    }
    void Update()
    {
        Rotate();
        Move();
    }
        void Rotate()
    {
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");

        rotateToMouse.Rotate(MouseX, MouseY);
    }
    void Move()
    {
        // 플레이어 이동
        keyX = Input.GetAxisRaw("Horizontal");
        KeyZ = Input.GetAxisRaw("Vertical");
        movement.Move(new Vector3(keyX, 0, KeyZ));
    }
}
