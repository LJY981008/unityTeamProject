using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 격발 애니메이션 스크립트
 * 
 */
public class FireState : StateMachineBehaviour
{
    string objName;
    PlayerBody playerbox;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // 오브젝트의 이름을 가져와
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerbox = animator.transform.root.GetComponent<PlayerBody>();
        playerbox.nuckbackDir = new Vector3((-playerbox.transform.forward.x) * 2, 0f, (-playerbox.transform.forward.z) * 2);
        playerbox.nuckbackDest = playerbox.transform.position + playerbox.nuckbackDir;
        objName = animator.name.Replace("(Clone)", "");
        if (objName.Equals("Rifle"))
            GameManager.instance.shotCount++;
        if (GameManager.instance.shotCount == 15)
        {
            GameManager.instance.prevDamage = GameManager.instance.currentDamage;
            GameManager.instance.currentDamage *= 1.1f;
        }
        AudioClip fireClip;
        fireClip = AudioManager.instance.GetFireClip(objName);
        PlayerUpper player = animator.GetComponent<PlayerUpper>();
        player.PlayAudio(fireClip);
        player.shot();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(objName.Equals("Shotgun"))
            playerbox.NuckBack();
    }

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
