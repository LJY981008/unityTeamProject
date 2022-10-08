using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Phase2RunScript : StateMachineBehaviour
{
    Phase2Script phase2Script;
    BossScript bs;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase2Script = FindObjectOfType<Phase2Script>();
        bs = FindObjectOfType<BossScript>();
        if(bs.ironreaver01 != null)
        {
            Destroy(bs.PhaseTwoModel.Find("ironreaver01").gameObject);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //bs.PhaseTwoModel.Find("ironreaver01").gameObject.SetActive(false);
        phase2Script.moveToTarget();
        if (phase2Script.getDistanceToTarget() <= 7.0f)
        {
            animator.SetInteger("aniInt", 1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //bs.PhaseTwoModel.Find("ironreaver01").gameObject.SetActive(false);
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
