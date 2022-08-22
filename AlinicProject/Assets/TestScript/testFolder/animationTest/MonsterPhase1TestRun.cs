using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPhase1TestRun : StateMachineBehaviour
{
    InitMonsterTest _initmonster;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _initmonster = FindObjectOfType<InitMonsterTest>();
        _initmonster.randomInt = Random.Range(1, 3);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 타겟을 바라보고 이동
        _initmonster.moveToTarget();
        // 거리가 10.0f 이하거나 같을때 Idle으로 전환
        if(_initmonster.getDistanceToTarget() <= 10.0f)
        {
            animator.SetInteger("aniInt", 1);
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
