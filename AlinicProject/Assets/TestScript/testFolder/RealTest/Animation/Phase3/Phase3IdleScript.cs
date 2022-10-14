using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3IdleScript : StateMachineBehaviour
{
    Phase3Script phase3Script;
    int randomInt, randomskillInt;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase3Script = FindObjectOfType<Phase3Script>();
        randomInt = Random.Range(3, 6);
        randomskillInt = Random.Range(6,8);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (phase3Script.target != null)
        {
            if (phase3Script.getDistanceToTarget() <= 13.0f)
            {
                 animator.SetInteger("aniInt", randomInt);
            }
            else
            {
                animator.SetInteger("aniInt", 2);
            }

            if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTime) >= 10.0f)
            {
                if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillOneTime) >= 10.0f
                    && phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTwoTime) >= 10.0f)
                {
                    animator.SetInteger("aniInt", randomskillInt);
                }
                else
                {
                    if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillOneTime) >= 15.0f)
                    {
                        animator.SetInteger("aniInt", 6);
                    }
                    else if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTwoTime) >= 15.0f)
                    {
                        animator.SetInteger("aniInt", 7);
                    }
                }
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
