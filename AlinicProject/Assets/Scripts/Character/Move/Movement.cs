using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-22
 * 내용 : 캐릭터의 이동 스크립트
 */
 [RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour 
{
    public static Movement instance;
    [SerializeField]
    private float mSpeed;   //이동속도
    private Vector3 mForce; // 이동하는 힘 (x,z와 y축을 별로도 계산해 실제 이동에 적용

    private void Awake() 
    {
        if (instance == null) instance = this;
        charaterController = GetComponent<CharacterController>();
    }
    public float MSpeed
    {
        set => mSpeed = Mathf.Max(0, value);
        get => mSpeed; 
    }
    private CharacterController charaterController; // 플레이어의 이동 제어를 위한 컴포넌트
    private void Update()
    {
        // 1초당 mForce 속력으로 이동
        charaterController.Move(mForce * Time.deltaTime);
    }
    public void Move(Vector3 dir)
    {
        // 이동 범위 = 캐릭터의 회전 값 * 방향값
        dir = transform.rotation * new Vector3(dir.x, 0, dir.z);
        // 이동 힘 = 이동방향 * 속도
        mForce = new Vector3(dir.x * mSpeed, mForce.y, dir.z* mSpeed);
    }
}