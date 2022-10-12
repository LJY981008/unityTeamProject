using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Script : MonoBehaviour
{
    BossScript bossScript;
    private GameObject _target;
    private string _targetName = "Player";
    private float _speedRun;
    private float _latelyCastSkillTime, _latelyCastSkillOneTime, _latelyCastSkillTwoTime;
    Vector3 targetPos;

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

    public float latelyCastSkillOneTime
    {
        get { return _latelyCastSkillOneTime; }
        set { _latelyCastSkillOneTime = value; }
    }

    public float latelyCastSkillTwoTime
    {
        get { return _latelyCastSkillTwoTime; }
        set { _latelyCastSkillTwoTime = value; }
    }
    private void Awake()
    {
        _speedRun = 3.5f;
        latelyCastSkillOneTime = -25.0f;
        latelyCastSkillTwoTime = -25.0f;
        bossScript = FindObjectOfType<BossScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = bossScript.deadPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (_target == null)
        {
            searchTarget();
        }
        else if (_target != null)
        {
            targetPos = target.transform.position;
        }

    }

    public void searchTarget()
    {
        Debug.Log("searchTarget()");
        // 몬스터 주변의 오브젝트 불러오기
        Collider[] colls = Physics.OverlapSphere(transform.position, 300.0f);
        // 몬스터 주변에 오브젝트가 있으면..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];
            // 캐릭터가 맞으면, 타겟 설정
            if (tmpColl.gameObject.name.Equals(_targetName))
            {
                // 타겟 오브젝트에 넣어주기
                target = tmpColl.gameObject;
                Debug.Log(target);
                break;
            }
        }
    }

    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // 몬스터가 타겟을 바라보기
        transform.LookAt(target.transform.position);
        // 몬스터를 타겟에 접근하기
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * _speedRun);
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void initSkillTime()
    {
        Debug.Log("skill init");
        latelyCastSkillTime = Time.time;
    }

    public void initSkillOneTime()
    {
        Debug.Log("skill one init");
        latelyCastSkillOneTime = Time.time;
    }

    public void initSkillTwoTime()
    {
        Debug.Log("skill two init");
        latelyCastSkillTwoTime = Time.time;
    }

    public float getDistanceOfTime(float currentTime, float latelyTime)
    {
        return currentTime - latelyTime;
    }
}
