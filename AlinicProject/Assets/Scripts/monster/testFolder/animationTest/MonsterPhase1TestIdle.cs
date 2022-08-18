using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterPhase1TestIdle : StateMachineBehaviour
{
    InitMonsterTest _initmonster;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _initmonster = FindObjectOfType<InitMonsterTest>();
        // 3,4 ��������
        _initmonster.randomInt = Random.Range(3, 5);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ÿ���� ������
        if (_initmonster.target == null)
        {
            Debug.Log("null");
        }
        // Ÿ���� ������
        if (_initmonster.target != null)
        {
            // Ÿ�ٰ��� �Ÿ� ��
            if(_initmonster.getDistanceToTarget() <= 10.0f)
            {
                //Debug.Log(_initmonster.getDistanceOfTime(Time.time, _initmonster.latelyCastSkillTime));
                // ���� �ð� - ��ų ���� �ð� �� 20�ʺ��� ���� ��
                if (_initmonster.getDistanceOfTime(Time.time, _initmonster.latelyCastSkillTime) >= 20.0f)
                {
                    animator.SetInteger("aniInt", 5);
                    // ��ų ���
                    _initmonster.castSkill();
                }
                else
                {
                    animator.SetInteger("aniInt", _initmonster.randomInt);
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
