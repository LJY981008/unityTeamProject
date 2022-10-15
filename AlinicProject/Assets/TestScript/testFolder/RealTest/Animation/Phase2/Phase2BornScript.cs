using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2BornScript : StateMachineBehaviour
{
    BossScript bossScript;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript = FindObjectOfType<BossScript>();
        animator.transform.LookAt(bossScript.target.transform.position);
        bossScript.changeAnimator(2);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UIManager.instance.bossHpFill.fillAmount = stateInfo.normalizedTime;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("1_Idle") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            animator.SetInteger("aniInt", 1);
            UIManager.instance.bossHpFill.fillAmount = 1.0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
