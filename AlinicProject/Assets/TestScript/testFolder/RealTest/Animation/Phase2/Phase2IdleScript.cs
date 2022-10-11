using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2IdleScript : StateMachineBehaviour
{
    Phase2Script phase2Script;
    int randomInt;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase2Script = FindObjectOfType<Phase2Script>();
        randomInt = Random.Range(3, 5);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (phase2Script.target != null)
        {
            if (phase2Script.getDistanceToTarget() <= 7.0f)
            {
                animator.SetInteger("aniInt", randomInt);
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
