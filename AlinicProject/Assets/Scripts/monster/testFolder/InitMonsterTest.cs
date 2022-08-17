using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMonsterTest : MonoBehaviour
{
    // 목표물을 담을 변수
    private GameObject _target;

    // 목표물의 Name
    private string _targetName = "Player";

    // Run 모션에서의 속도
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
        // 타겟이 없을때는 검색함수 실행
        if(_target == null)
        {
            Debug.Log("searching...");
            searchTarget();
        }
        //타겟이 존재할때 
        else if(_target != null)
        {
            Debug.Log("player detected");
            //moveToTarget();
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

        // 몬스터가 타겟을 바라보기
        transform.LookAt(target.transform.position);
        // 몬스터를 타겟에 접근하기
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
