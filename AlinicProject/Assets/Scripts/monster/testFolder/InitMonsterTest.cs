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

    int _randomInt;
    public float hp = 0.0f;
    public bool isDelay;
    public float delayTime = 10.0f;

    int _skillTrigger = 0;

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

    public int randomInt
    {
        get { return _randomInt; }
        set { _randomInt = value; }
    }

    public int skillTrigger
    {
        get { return skillTrigger; }
        set { skillTrigger = value; }
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
        hp = hp + Time.deltaTime * 0.1f;
        // Ÿ���� �������� �˻��Լ� ����
        if(_target == null)
        {
            Debug.Log("searching...");
            searchTarget();
        }
        //Ÿ���� �����Ҷ� 
        else if(_target != null)
        {
            Debug.Log("player detected");
            //moveToTarget();
        }
    }
    public void searchTarget()
    {
        Debug.Log("searchTarget()");
        // ���� �ֺ��� ������Ʈ �ҷ�����
        Collider[] colls = Physics.OverlapSphere(transform.position, 300.0f);
        // ���� �ֺ��� ������Ʈ�� ������..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];
            // ĳ���Ͱ� ������, Ÿ�� ����
            if (tmpColl.gameObject.name.Equals(_targetName))
            {
                // Ÿ�� ������Ʈ�� �־��ֱ�
                target = tmpColl.gameObject;
                break;
            }
        }
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.transform.position);
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

    IEnumerator hpTest()
    {
        yield return new WaitForSeconds(1.0f);
        
    }
    IEnumerator usingSkill()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        isDelay = false;
    }
}
