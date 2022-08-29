using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // 오브젝트의 이름을 가져와
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string objName = animator.name.Replace("(Clone)", "");
        AudioClip fireClip;
        fireClip = Ex_AudioManager.instance.GetFireClip(objName);
        PlayerUpper.instance.PlayAudio(fireClip);
        switch (objName)
        {
            case "Rifle":;
                PlayerUpper.instance.shot(0);
                break;
            case "Pistol":
                PlayerUpper.instance.shot(1);
                break;
            case "ShotGun":
                PlayerUpper.instance.shot(2);
                break;
            default:
                Debug.Log("탄안됬어");
                break;
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    /*override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
*/
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    /*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }*/

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
