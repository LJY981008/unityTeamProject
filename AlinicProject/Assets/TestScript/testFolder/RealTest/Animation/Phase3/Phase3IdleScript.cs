using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3IdleScript : StateMachineBehaviour
{
    int randomInt, randomskillInt;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomInt = Random.Range(3, 6);
        randomskillInt = Random.Range(6,8);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (InitMonster.Instance.target != null)
        {
            if (InitMonster.Instance.getDistanceToTarget() <= 20.0f || animator.GetBool("ATTACK"))
            {
                animator.SetBool("ATTACK", false);
                animator.SetInteger("aniInt", randomInt);
            }
            else
            {
                animator.SetInteger("aniInt", 2);
            }

            if (InitMonster.Instance.getDistanceOfTime(Time.time, InitMonster.Instance.latelyCastSkillTime) >= 10.0f)
            {
                if (InitMonster.Instance.getDistanceOfTime(Time.time, InitMonster.Instance.latelyCastSkillOneTime) >= 15.0f
                    && InitMonster.Instance.getDistanceOfTime(Time.time, InitMonster.Instance.latelyCastSkillTwoTime) >= 15.0f)
                {
                    animator.SetInteger("aniInt", randomskillInt);
                }
                else
                {
                    if (InitMonster.Instance.getDistanceOfTime(Time.time, InitMonster.Instance.latelyCastSkillOneTime) >= 15.0f)
                    {
                        animator.SetInteger("aniInt", 6);
                    }
                    else if (InitMonster.Instance.getDistanceOfTime(Time.time, InitMonster.Instance.latelyCastSkillTwoTime) >= 15.0f)
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
