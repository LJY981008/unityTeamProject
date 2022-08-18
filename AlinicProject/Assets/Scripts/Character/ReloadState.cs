using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // ���� �߿� �߻��ϸ� ���� ���, �ִϸ��̼��� ������ idle�� ��ȯ
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("isFire") == true)
        {
            animator.SetBool("isReload", false);
        }
        else if(stateInfo.normalizedTime > 1.0f)
        {
            animator.SetBool("isReload", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // ������Ʈ�� �̸��� ������ �ش��ϴ� ź�� ����
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string objName = animator.name.Replace("(Clone)", "");
        switch (objName)
        {
            case "Rifle":
                PlayerUpper.instance.ReloadAmmo(0);
                break;
            case "Pistol":
                PlayerUpper.instance.ReloadAmmo(1);
                break;
            case "ShotGun":
                PlayerUpper.instance.ReloadAmmo(2);
                break;
            default:
                Debug.Log("ź�ȉ��");
                break;
        }
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
