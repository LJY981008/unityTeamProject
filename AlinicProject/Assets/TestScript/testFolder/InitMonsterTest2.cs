using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InitMonsterTest2 : MonoBehaviour
{
    // Run 모션에서의 속도
    private float _speedRun;
    public float speedRun
    {
        get { return _speedRun; }
        set { _speedRun = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // 몬스터가 타겟을 바라보기
        //transform.LookAt(target.transform.position);
        // 몬스터를 타겟에 접근하기
        //transform.position =
        //Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }
}
