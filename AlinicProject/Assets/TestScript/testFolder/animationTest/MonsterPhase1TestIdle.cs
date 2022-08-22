using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterPhase1TestIdle : StateMachineBehaviour
{
    InitMonsterTest _initmonster;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _initmonster = FindObjectOfType<InitMonsterTest>();
        // 3,4 랜덤변수
        _initmonster.randomInt = Random.Range(3, 5);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 타겟이 없을때
        if (_initmonster.target == null)
        {
            Debug.Log("null");
        }
        // 타겟이 있을때
        if (_initmonster.target != null)
        {
            // 타겟과의 거리 비교
            if(_initmonster.getDistanceToTarget() <= 10.0f)
            {
                //Debug.Log(_initmonster.getDistanceOfTime(Time.time, _initmonster.latelyCastSkillTime));
                // 현재 시간 - 스킬 시전 시간 이 20초보다 높을 때
                if (_initmonster.getDistanceOfTime(Time.time, _initmonster.latelyCastSkillTime) >= 20.0f)
                {
                    animator.SetInteger("aniInt", 5);
                    // 스킬 사용
                    _initmonster.castSkill();
                }
                else
                {
                    animator.SetInteger("aniInt", _initmonster.randomInt);
                }
            }
            else
            {
                animator.SetInteger("aniInt", 2);
            }
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
