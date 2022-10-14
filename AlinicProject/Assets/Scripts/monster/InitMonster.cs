using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 몬스터의 관한 전반적인 코드를 담는 클래스
/// </summary>

public class InitMonster : MonoBehaviour
{

    private static InitMonster instance = null;
    System.Random random;

    // 데미지 텍스쳐
    GameObject hudDamageText;

    public static InitMonster Instance
    {
        get
        {
            if (instance == null) { return null; }

            return instance;
        }
    }

    /// <summary>
    /// true : 타겟을 설정한다.
    /// false : 설정하지 않는다.
    /// </summary>
    bool isSettingTarget = true;

    /// <summary>
    /// true : 무적
    /// fasle : 무적 해제
    /// </summary>
    bool isInvincibility = true;


    /// <summary>
    /// true : 로그 찍기
    /// false : 안찍기
    /// </summary>
    bool isDebugging = true;

    /// <summary>
    /// true : 관리자 키 활성화
    /// </summary>
    bool isAdminMod = true;

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
        set { _phaseState = value; }
    }

    /// <summary>
    /// 현재 페이즈를 변경한다.
    /// </summary>
    /// <param name="state">페이즈</param>
    /// <returns></returns>
    public Boolean setPhaseState(int state)
    {
        if(state == 0
            || state == 1
            || state == 2)
        {
            _phaseState = state;
            monsterHp = PHASE_HP[state];

            if(isDebugging) { Debug.Log("### 체력 설정 : " + PHASE_HP[state]); }
                

            return true;
        }
        return false;
    }

    /// <summary>
    /// 페이즈별 체력을 담는 배열 변수
    /// </summary>
    private static int[] PHASE_HP = { 1000, 2000, 3000 };

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
        if (instance == null)
        {
            instance = this;
        }

        random = new System.Random();
        hudDamageText = GameObject.FindWithTag("hudDamageText");

        // 초기 페이즈 설정
        setPhaseState(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAdminMod)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                onDamage(50);
            }
        }
        
    }

    /// <summary>
    /// 몬스터 주변 캐릭터 찾기
    /// </summary>
    public void searchTarget()
    {

        if(_target == null)
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
                    settingTarget(tmpColl.gameObject);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 타겟 설정하기
    /// </summary>
    public void settingTarget(GameObject gObj)
    {
        if(isSettingTarget)
        {
            if(isDebugging) { Debug.Log("### 타겟이 설정됨"); }
            _target = gObj;
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
    /// 몬스터와 타겟까지의 거리 구하기
    /// </summary>
    public float getDistanceToTarget(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, target.transform.position);
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

        //
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

        if(isInvincibility) { return; }

        if(target == null)
        {
            settingTarget(GameObject.FindGameObjectWithTag("Character"));
        }

        showDamageText(amountOfDamage);
        monsterHp = monsterHp - amountOfDamage;

        // 몬스터가 피해를 받고 페이즈 변경 처리
        if(monsterHp <= 0 && !isInvincibility)
        {
            startInvincibility();

            if (phaseState == 0) // 1페이즈 종료
            {
                if (isDebugging)
                {
                    Debug.Log("### 1페이즈 종료");
                }
                setPhaseState(1);
                BossScript.instance.closePhase1();
            }
            else if (phaseState == 1) // 1페이즈 종료
            {
                if (isDebugging)
                {
                    Debug.Log("### 2페이즈 종료");
                }
                setPhaseState(2);
                BossScript.instance.closePhase2();
            }
            else if (phaseState == 2) // 1페이즈 종료
            {
                if (isDebugging)
                {
                    Debug.Log("### 3페이즈 종료");
                }
                BossScript.instance.closePhase3();
            }
        }


    }

    /// <summary>
    /// 피해를 입힐 경우 데미지 텍스트 처리하기
    /// </summary>
    /// <param name="damage"></param>

    public void showDamageText(int damage)
    {

        Transform hudPos = CameraController.instance.playerBox.transform;

        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.SetParent(GameObject.Find("Canvas").transform);
        hudText.transform.localPosition = new Vector3(200 + random.Next(100), 80, -30);
        hudText.AddComponent<DamageText>();
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달

    }

    /// <summary>
    /// 무적을 설정한다.
    /// </summary>
    public void startInvincibility()
    {
        if (isDebugging)
        {
            Debug.Log("### 무적 ###");
        }
        isInvincibility = true;
    }

    /// <summary>
    /// 무적을 해제한다.
    /// </summary>
    public void closeInvincibility()
    {
        if (isDebugging)
        {
            Debug.Log("### 무적 해제 ###");
        }
        isInvincibility = false;
    }

}
