using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPhase : MonoBehaviour
{

    private void Awake()
    {
        transform.parent.parent.GetComponent<InitMonster>().animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Animator getAnimator()
    {
        return GetComponent<Animator>();
    }
}
