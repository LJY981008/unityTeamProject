using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InitMonsterTest2 : MonoBehaviour
{
    // Run ��ǿ����� �ӵ�
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

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        //transform.LookAt(target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        //transform.position =
        //Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }
}
