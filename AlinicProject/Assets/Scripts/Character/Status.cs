using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자 : 임석현
 * 마지막 수정 : 2022-08-22
 * 내용 : 캐릭터 달리기 걷기 조작
 */
public class Status : MonoBehaviour 
{
    [Header("Walk, Run Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    private void Awake() 
    {
    }
}