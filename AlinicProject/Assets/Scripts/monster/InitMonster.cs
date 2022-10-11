using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

/// <summary>
/// 몬스터의 관한 전반적인 코드를 담는 클래스
/// </summary>

public class InitMonster : MonoBehaviour
{

    /// <summary>
    /// 현재 페이즈 상태를 담는 변수
    /// 0 : 1페이즈
    /// 1 : 2페이즈
    /// 2 : 3페이즈
    /// </summary>
    private int _phaseState;

    /// <summary>
    /// 현재 페이즈 상태를 담는 변수
    /// set : 상태 변경 시 현재 생명력을 페이즈별 생명력으로 주입 시켜준다.
    /// </summary>
    public int phaseState
    {
        get { return _phaseState; }
    }

    /// <summary>
    /// 현재 페이즈를 변경한다.
    /// </summary>
    /// <param name="state">페이즈</param>
    /// <returns></returns>
    public Boolean setPhaseState(int state)
    {
        if(state == 0
            && state == 1
            && state == 2)
        {
            _phaseState = state;
            monsterHp = PHASE_HP[state];
            return true;
        }
        return false;
    }

    /// <summary>
    /// 페이즈별 체력을 담는 배열 변수
    /// </summary>
    private static int[] PHASE_HP = { 10000, 20000, 30000 };

    /// <summary>
    /// 몬스터의 현재 체력을 담을 변수
    /// </summary>
    private int _monsterHp;

    /// <summary>
    /// 몬스터의 현재 체력을 담을 변수
    /// </summary>
    public int monsterHp
    {
        get { return _monsterHp; }
        set { _monsterHp = value;}
    }

    // 이펙트 관련 변수
    public GameObject[] Effects;
    private GameObject effectToSpawn;


    // 목표물을 담을 변수
    private GameObject _target;

    // 목표물의 Name
    private string _targetName = "Character";

    // Run 모션에서의 속도
    private float _speedRun;

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

        // 초기 페이즈 설정
        setPhaseState(0);

        // 미사일 이펙트 설정
        effectToSpawn = Effects[0];
    }

    // Update is called once per frame
    void Update()
    {
        // 미사일 날리기 예제
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackHandRazer();
        }

        // 설정된 타겟이 없으면
        if (_target == null)
        {
            searchTarget();
        }

        // 설정된 타겟이 있으면...
        if (_target != null)
        {

        }
    }

    /// <summary>
    /// 몬스터 주변 캐릭터 찾기
    /// </summary>
    void searchTarget()
    {
        // 몬스터 주변의 오브젝트 불러오기
        Collider[] colls = Physics.OverlapSphere(transform.position, 200.0f);

        // 몬스터 주변에 오브젝트가 있으면..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];

            // 캐릭터가 맞으면, 타겟 설정
            if (tmpColl.gameObject.name.Equals(_targetName))
            {
                // 타겟 오브젝트에 넣어주기
                _target = tmpColl.gameObject;
                break;
            }
        }
    }

    /// <summary>
    /// 몬스터와 타겟까지의 거리 구하기
    /// </summary>
    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    /// <summary>
    /// 몬스터를 타겟으로 이동하기
    /// </summary>
    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");
        
        // 몬스터가 타겟을 바라보기
        transform.LookAt(target.transform.position);
        // 몬스터를 타겟에 접근하기
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }

    /// <summary>
    /// 손미사일 공격
    /// </summary>
    void attackHandRazer()
    {

        //TODO: 손미사일이 나가는 위치 : 3페이즈 bone046 처리해줘야 함
        GameObject Effects;

        if (_target != null)
        {
            Effects = Instantiate(effectToSpawn, transform.position, Quaternion.identity);
            Effects.transform.localRotation = getRotation(gameObject, target.transform.position);
        }
        else
        {
            Debug.Log("No Target");
        }
    }

    /// <summary>
    /// 투사체 Quaternion 구하는 함수.
    /// </summary>
    Quaternion getRotation(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        return Quaternion.LookRotation(direction);
    }


    /// <summary>
    /// 몬스터에게 피해를 입힐 때 사용하는 메소드.
    /// </summary>
    /// <param name="amountOfDamage">피해량 수치</param>
    public void onDamage(int amountOfDamage)
    {
        monsterHp = monsterHp - amountOfDamage;

        //TODO: 몬스터가 피해를 받고 페이즈 변경 처리

    }

}
