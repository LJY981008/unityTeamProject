using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1BornScript : StateMachineBehaviour
{

    private Boolean flagAnimation = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!flagAnimation && InitMonster.Instance.target != null)
        {
            flagAnimation = true;
            InitMonster.Instance.showChangeCamera1();
        } 
        else
        {
            //Debug.Log("Å¸°Ù ÁöÁ¤ ¾È µÊ");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        InitMonster.Instance.closeChangeCamera1();
        InitMonster.Instance.closeInvincibility();
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
