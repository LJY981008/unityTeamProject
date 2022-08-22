using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-22
 * 내용 : 캐릭터의 카메라 이동 구현 스크립트
 */
public class RotateToMouse_ : MonoBehaviour 
{
    public static RotateToMouse_ instance;
    float rotCamXAxisSpeed = 5; // 카메라 X축 회전속도
    float rotCamYAxisSpeed = 3; // 카메라 y축 회전속도
    float eulerAnglesX, eulerAnglesY;
    private void Awake()
    {
        if (instance == null)   instance = this;
    }
    public void Rotate(float MouseX, float MouseY)
    {
        eulerAnglesY += MouseX * rotCamYAxisSpeed;
        eulerAnglesX -= MouseY * rotCamXAxisSpeed;
        // 카메라 x축의 경우 회전의 범위를 설정
        eulerAnglesX = Mathf.Clamp(eulerAnglesX, -40f, 40f);
        transform.rotation = Quaternion.Euler(eulerAnglesX, eulerAnglesY, 0);
    }
}