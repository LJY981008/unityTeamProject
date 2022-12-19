using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1SklScript : StateMachineBehaviour
{

    MonsterAttack monsterAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(InitMonster.Instance.target.transform.position);
        damageTime = Time.time + 2;
        monsterAttack = new MonsterAttack(0, 10);
        monsterAttack.startAttackArea();

    }

    float damageTime;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!(InitMonster.Instance.getDistanceOfTime(Time.time, damageTime) - 1f < 0) && InitMonster.Instance.getDistanceToTarget() >= 9.5f)
        {
            InitMonster.Instance.moveToTarget(InitMonster.Instance.PhaseOneModel, 0.35f);
        }
        if (InitMonster.Instance.getDistanceToTarget() <= 10.0f)
        {
            Debug.Log(InitMonster.Instance.getDistanceOfTime(Time.time, damageTime));
            if (InitMonster.Instance.getDistanceOfTime(Time.time, damageTime) > 1f)
            {
                damageTime = Time.time;
                GameManager.instance.playerBody.OnDamage(10);
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateExit - skill");
        monsterAttack.destroyAttack();
        InitMonster.Instance.initSkillTime();
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
