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
    [SerializeField]
    private float mSpeed;   //이동속도
    private Vector3 mForce; // 이동하는 힘 (x,z와 y축을 별로도 계산해 실제 이동에 적용
    [SerializeField]
    private float jForce;   // 점프의 힘
    [SerializeField]
    private float gravity;   // 중력 계수

    private void Awake() 
    {
        charaterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
        Minimap.instance.SetPlayerMapFocus(transform.rotation.eulerAngles);
    }
    public float MSpeed
    {
        set => mSpeed = Mathf.Max(0, value);
        get => mSpeed; 
    }
    private CharacterController charaterController; // 플레이어의 이동 제어를 위한 컴포넌트
    private void Update()
    {
        // 허공에 떠 있으면 중력만큼 y축 이동속도 감소
        if ( !charaterController.isGrounded )
        {
            mForce.y += gravity * Time.deltaTime;
        }
        // 1초당 mForce 속력으로 이동
        charaterController.Move(mForce * Time.deltaTime);
        Minimap.instance.MovePlayerMap(transform.position);
    }
    public void Move(Vector3 dir)
    {
        // 이동 범위 = 캐릭터의 회전 값 * 방향값
        dir = transform.rotation * new Vector3(dir.x, 0, dir.z);
        // 이동 힘 = 이동방향 * 속도
        mForce = new Vector3(dir.x * mSpeed, mForce.y, dir.z* mSpeed);
    }
    public void Jump()
    {
        // 플레이어가 바닥에 있을때만 점프가 가능
        if ( charaterController.isGrounded )
        {
            mForce.y = jForce;
        }
    }
}