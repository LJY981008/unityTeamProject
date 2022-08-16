using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMonsterTest : MonoBehaviour
{
    // ��ǥ���� ���� ����
    private GameObject _target;

    // ��ǥ���� Name
    private string _targetName = "Player";

    // Run ��ǿ����� �ӵ�
    private float _speedRun;

    float distanceOfPlayer;

    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }

    public float speedRun
    {
        get { return _speedRun; }
        set { _speedRun = value; }
    }

    private void Awake()
    {
        _speedRun = 15.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // Ÿ���� �������� �˻��Լ� ����
        if(_target == null)
        {
            Debug.Log("searching...");
            searchTarget();
        }
        //Ÿ���� �����Ҷ� �Ÿ��� ���Ͽ� 120 �����϶� �Լ�����
        else if(_target != null)
        {
            distanceOfPlayer = getDistanceToTarget();
            if(distanceOfPlayer <= 120f)
            {
                moveToTarget();
            }
        }
    }
    void searchTarget()
    {
        // ���� �ֺ��� ������Ʈ �ҷ�����
        Collider[] colls = Physics.OverlapSphere(transform.position, 200.0f);

        // ���� �ֺ��� ������Ʈ�� ������..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];
            // ĳ���Ͱ� ������, Ÿ�� ����
            if (tmpColl.gameObject.name.Equals(_targetName))
            {
                // Ÿ�� ������Ʈ�� �־��ֱ�
                _target = tmpColl.gameObject;
                break;
            }
        }
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        transform.LookAt(target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }
}
