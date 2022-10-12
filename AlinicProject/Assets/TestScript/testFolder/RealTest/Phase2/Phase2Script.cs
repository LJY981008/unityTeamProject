using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Script : MonoBehaviour
{
    BossScript bossScript;
    private GameObject _target;
    private string _targetName = "Player";
    private float _speedRun;
    private float _latelyCastSkillTime;
    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }
    public float latelyCastSkillTime
    {
        get { return _latelyCastSkillTime; }
        set { _latelyCastSkillTime = value; }
    }
    private void Awake()
    {
        bossScript = FindObjectOfType<BossScript>();
        _speedRun = 3.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = bossScript.deadPosition;
    }

    // Update is called once per frame
    void Update()
    {
        InitMonster.Instance.searchTarget();
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
                Debug.Log(target);
                break;
            }
        }
    }
    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        transform.LookAt(target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * _speedRun);
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

}
