using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPhase1Atk : StateMachineBehaviour
{
    InitMonster _monster;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _monster = FindObjectOfType<InitMonster>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("test");
    }
}
