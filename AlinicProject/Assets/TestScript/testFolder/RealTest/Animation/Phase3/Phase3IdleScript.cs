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
        randomInt = Random.Range(3, 7);
        randomskillInt = Random.Range(7,9);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (phase3Script.target != null)
        {
            if (phase3Script.getDistanceToTarget() <= 7.0f)
            {
                Debug.Log("1");
                if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTime) >= 15.0f)
                {
                    Debug.Log("2");
                    if (phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillOneTime) >= 20.0f
                        && phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTwoTime) >= 20.0f)
                    {
                        Debug.Log("3");
                        animator.SetInteger("aniInt", randomskillInt);
                    }
                    else
                    {
                        if(phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillOneTime) >= 20.0f)
                        {
                            Debug.Log("4");
                            animator.SetInteger("aniInt", 7);
                        }
                        else if(phase3Script.getDistanceOfTime(Time.time, phase3Script.latelyCastSkillTwoTime) >= 20.0f)
                        {
                            Debug.Log("5");
                            animator.SetInteger("aniInt", 8);
                        }
                    }
                }
                else
                {
                    animator.SetInteger("aniInt", randomInt);
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
