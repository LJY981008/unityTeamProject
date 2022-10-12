using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static BossScript instance;
    GameObject _target;
    public Transform PhaseOneBoss, PhaseOneModel, PhaseTwoBoss, PhaseTwoModel, PhaseThreeBoss, PhaseThreeModel;
    Animator ani;
    int _triggerInt;
    Phase1Script phase1Script;
    Vector3 _deadPosition;
    public GameObject ironreaver01;
    List<Transform> childTs;
    Vector3 _targetPos;


    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }

    public int triggerInt
    {
        get { return _triggerInt; }
        set { _triggerInt = value; }
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


        childTs = new List<Transform>();
        for (int i = 0; i < 3; i++)
        {
            childTs.Add(transform.GetChild(i));
        }

        triggerInt = 1;
        target = GameObject.Find("Player");
        PhaseOneBoss = childTs[0];
        PhaseOneModel = childTs[0].Find("1_Model");
        PhaseTwoBoss = childTs[1];
        PhaseTwoModel = childTs[1].Find("2_Model");
        PhaseThreeBoss = childTs[2];
        PhaseThreeModel = childTs[2].Find("3_Model");
        ani = PhaseOneModel.GetComponent<Animator>();
        ironreaver01 = PhaseTwoModel.Find("ironreaver01").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 범위내에 진입했을때 보스 출현
        if(getDistanceToTarget() <= 50.0f)
        {
            // Debug.Log("distanceTest");
            if(_triggerInt == 1)
            {
                PhaseOneBoss.gameObject.SetActive(true);
            }
        }

        if (Input.GetKey(KeyCode.F6))
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
        }
        if (_triggerInt == 2)
        {
            PhaseOneBoss.gameObject.SetActive(false);

            PhaseTwoBoss.gameObject.SetActive(true);
        }
        if(_triggerInt == 3)
        {
            PhaseTwoBoss.gameObject.SetActive(false);

            PhaseThreeBoss.gameObject.SetActive(true);
        }

        _targetPos = _target.transform.position;
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void doInvoke()
    {
        Invoke("makeBossDie", 5.5f);
    }
    void makeBossDie()
    {
        _triggerInt = 2;
    }

    public void doInvoke2()
    {
        Invoke("makeBossDie2", 0.3f);
    }
    void makeBossDie2()
    {
        _triggerInt = 3;
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

}
