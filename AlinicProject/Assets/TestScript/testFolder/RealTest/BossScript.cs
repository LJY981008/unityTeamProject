using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static BossScript instance;
    GameObject _target;

    List<Transform> childTs;
    public Transform PhaseOneBoss, PhaseOneModel, PhaseTwoBoss, PhaseTwoModel, PhaseThreeBoss, PhaseThreeModel;
    public GameObject ironreaver01;
    Animator ani;
    Vector3 _deadPosition;
    Vector3 _targetPos;

    private float _latelyCastSkillTime, _latelyCastSkillOneTime, _latelyCastSkillTwoTime;
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


    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }

    public Vector3 deadPosition
    {
        get { return _deadPosition; }
    }

    public Animator bossAni
    {
        get { return ani; }
    }

    public Vector3 targetPos
    {
        get { return _targetPos; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        //target = GameObject.Find("Player");

        childTs = new List<Transform>();
        for (int i = 0; i < 3; i++)
        {
            childTs.Add(transform.GetChild(i));
        }
        
        PhaseOneBoss = childTs[0];
        PhaseOneModel = childTs[0].Find("1_Model");
        PhaseTwoBoss = childTs[1];
        PhaseTwoModel = childTs[1].Find("2_Model");
        PhaseThreeBoss = childTs[2];
        PhaseThreeModel = childTs[2].Find("3_Model");
        ani = PhaseOneModel.GetComponent<Animator>();
        ironreaver01 = PhaseTwoModel.Find("ironreaver01").gameObject;

        latelyCastSkillOneTime = -25.0f;
        latelyCastSkillTwoTime = -25.0f;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 범위내에 진입했을때 보스 출현
        /*if(InitMonster.Instance.getDistanceToTarget() <= 50.0f)
        {
            // Debug.Log("distanceTest");
            if(_triggerInt == 1)
            {
                PhaseOneBoss.gameObject.SetActive(true);
            }
        }*/

        /*if (Input.GetKey(KeyCode.F6))
        {
            ani.SetInteger("aniInt", 6);
            //Debug.Log(_deadPosition);
        }
        if (Input.GetKey(KeyCode.F7))
        {
            Debug.Log("f7");
            Debug.Log(ani);
            ani.SetInteger("aniInt", 5);
            //Debug.Log(_deadPosition);
        }
        if (Input.GetKey(KeyCode.F8))
        {
            ani.SetInteger("aniInt", 9);
        }*/

        _targetPos = _target.transform.position;
    }


    

    public void ActiveChangeInvoke()
    {
        Invoke("ActiveChangePhase2", 5.5f);
    }

    public void ActiveChangeInvoke2()
    {
        Invoke("ActiveChangePhase3", 0.3f);
    }

    void ActiveChangePhase2()
    {
        PhaseOneBoss.gameObject.SetActive(false);

        PhaseTwoBoss.gameObject.SetActive(true);
    }

    void ActiveChangePhase3()
    {
        PhaseOneBoss.gameObject.SetActive(false);

        PhaseTwoBoss.gameObject.SetActive(true);
    }


    public void rememberDeadPosition()
    {
        Debug.Log(PhaseOneModel.transform.position);
        _deadPosition = PhaseOneModel.transform.position;
    }

    public void rememberTwoDeadPosition()
    {
        Debug.Log(PhaseTwoModel.transform.position);
        _deadPosition = PhaseTwoModel.transform.position;
    }

    public void changeAnimator(int phase)
    {
        if(phase == 2)
        {
            Debug.Log("ani changed - 2");
            ani = PhaseTwoModel.GetComponent<Animator>();
        }
        else if(phase == 3)
        {
            Debug.Log("ani changed - 3");
            ani = PhaseThreeModel.GetComponent<Animator>();
        }
    }


    /// <summary>
    /// 1페이즈 종료
    /// </summary>
    public void closePhase1()
    {
        ani.SetInteger("aniInt", 6);
    }

    /// <summary>
    /// 2페이즈 종료
    /// </summary>
    public void closePhase2()
    {
        ani.SetInteger("aniInt", 5);
    }

    /// <summary>
    /// 3페이즈 종료
    /// </summary>
    public void closePhase3()
    {
        ani.SetInteger("aniInt", 9);
    }

    public void initSkillTime()
    {
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

    public void moveToTarget(Transform boss)
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // 몬스터가 타겟을 바라보기
        boss.LookAt(InitMonster.Instance.target.transform.position);
        // 몬스터를 타겟에 접근하기
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * InitMonster.Instance.speedRun);
    }

}
