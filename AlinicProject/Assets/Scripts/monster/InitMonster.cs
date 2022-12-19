using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;/*
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;*/

/// <summary>
/// ������ ���� �������� �ڵ带 ��� Ŭ����
/// </summary>

public class InitMonster : MonoBehaviour
{

    private static InitMonster instance = null;
    public System.Random random;

    // ������ �ؽ���
    GameObject hudDamageText;

    public static InitMonster Instance
    {
        get
        {
            if (instance == null) { return null; }

            return instance;
        }
    }

    public Animator animator;

    /// <summary>
    /// true : Ÿ���� �����Ѵ�.
    /// false : �������� �ʴ´�.
    /// </summary>
    bool isSettingTarget = true;

    /// <summary>
    /// true : ����
    /// fasle : ���� ����
    /// </summary>
    bool isInvincibility = false;


    /// <summary>
    /// true : �α� ���
    /// false : �����
    /// </summary>
    bool isDebugging = true;

    /// <summary>
    /// true : ������ Ű Ȱ��ȭ
    /// </summary>
    bool isAdminMod = true;

    /// <summary>
    /// ���� ������ ���¸� ��� ����
    /// 0 : 1������
    /// 1 : 2������
    /// 2 : 3������
    /// </summary>
    private int _phaseState;

    /// <summary>
    /// ���� ������ ���¸� ��� ����
    /// set : ���� ���� �� ���� ������� ����� ��������� ���� �����ش�.
    /// </summary>
    public int phaseState
    {
        get { return _phaseState; }
        set { _phaseState = value; }
    }

    /// <summary>
    /// ���� ����� �����Ѵ�.
    /// </summary>
    /// <param name="state">������</param>
    /// <returns></returns>
    public Boolean setPhaseState(int state)
    {
        if(state == 0
            || state == 1
            || state == 2)
        {
            _phaseState = state;
            monsterHp = PHASE_HP[state];

            if(isDebugging) { Debug.Log("### ü�� ���� : " + PHASE_HP[state]); }
                

            return true;
        }
        return false;
    }

    /// <summary>
    /// ����� ü���� ��� �迭 ����
    /// </summary>
    private static int[] PHASE_HP = { 1000, 2000, 3000 };

    /// <summary>
    /// ������ ���� ü���� ���� ����
    /// </summary>
    private int _monsterHp;

    /// <summary>
    /// ������ ���� ü���� ���� ����
    /// </summary>
    public int monsterHp
    {
        get { return _monsterHp; }
        set { _monsterHp = value;}
    }

    // ����Ʈ ���� ����
    public GameObject[] Effects;
    private GameObject effectToSpawn;


    // ��ǥ���� ���� ����
    private GameObject _target;

    // ��ǥ���� Name
    private string _targetName = "Player";

    // Run ��ǿ����� �ӵ�
    private float _speedRun;
    private float _prevSpeedRun;

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
    // BossScript�� �ִ� ���������
    private List<Transform> childTs;
    private Transform _PhaseOneBoss, _PhaseOneModel, _PhaseTwoBoss, _PhaseTwoModel, _PhaseThreeBoss, _PhaseThreeModel;
    private GameObject _ironreaver01;
    private Animator ani;
    private Vector3 _deadPosition;
    private Vector3 _targetPos;
    private Vector3 _deadForward;

    public Transform PhaseOneModel
    {
        get { return _PhaseOneModel; }
    }
    public Transform PhaseTwoModel
    {
        get { return _PhaseTwoModel; }
    }
    public Transform PhaseThreeModel
    {
        get { return _PhaseThreeModel; }
    }
    public GameObject ironreaver01
    {
        get { return _ironreaver01; }
    }
    public Vector3 targetPos
    {
        get { return _targetPos; }
    }
    public Vector3 deadForward
    {
        set { _deadForward = value; }
        get { return _deadForward; }
    }

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
    //�������

    private void Awake()
    {
        _speedRun = 15.0f;

        childTs = new List<Transform>();
        for (int i = 0; i < 3; i++)
        {
            childTs.Add(transform.GetChild(i));
        }

        _PhaseOneBoss = childTs[0];
        _PhaseOneModel = childTs[0].Find("1_Model");
        _PhaseTwoBoss = childTs[1];
        _PhaseTwoModel = childTs[1].Find("2_Model");
        _PhaseThreeBoss = childTs[2];
        _PhaseThreeModel = childTs[2].Find("3_Model");
        ani = _PhaseOneModel.GetComponent<Animator>();
        _ironreaver01 = _PhaseTwoModel.Find("ironreaver01").gameObject;
        latelyCastSkillOneTime = -25.0f;
        latelyCastSkillTwoTime = -25.0f;
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

        // �ʱ� ������ ����
        setPhaseState(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInvincibility) UIManager.instance.UpdateBossHp(monsterHp, PHASE_HP[phaseState]);
        if (isAdminMod)
        {
            /*if (Input.GetKeyDown(KeyCode.N))
            {
                actionSpeedDown(10);
            }
            */
            if (Input.GetKeyDown(KeyCode.M))
            {
                onDamage(10000);
            }
        }
        if(_target != null)
            _targetPos = _target.transform.position;
    }

    /// <summary>
    /// ���� �ֺ� ĳ���� ã��
    /// </summary>
    public void searchTarget()
    {

        if(_target == null)
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
                    settingTarget(tmpColl.gameObject);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Ÿ�� �����ϱ�
    /// </summary>
    public void settingTarget(GameObject gObj)
    {
        if(isSettingTarget)
        {
            if(isDebugging) { Debug.Log("### Ÿ���� ������"); }
            _target = gObj;
        }
    }

    /// <summary>
    /// ���Ϳ� Ÿ�ٱ����� �Ÿ� ���ϱ�
    /// </summary>
    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    /// <summary>
    /// ���Ϳ� Ÿ�ٱ����� �Ÿ� ���ϱ�
    /// </summary>
    public float getDistanceToTarget(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, target.transform.position);
    }

    /// <summary>
    /// ���͸� Ÿ������ �̵��ϱ�
    /// </summary>
    public void moveToTarget()
    {
        // Debug.Log("### InitMonster.moveToTarget ###");
        
        // ���Ͱ� Ÿ���� �ٶ󺸱�
        transform.LookAt(target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }
    
    // ���ο� �̵��Լ�
    public void moveToTarget(Transform boss)
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        boss.LookAt(InitMonster.Instance.target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * InitMonster.Instance.speedRun);
    }

    // ���ο� �̵��Լ�
    public void moveToTarget(Transform boss, float multip)
    {
        // Debug.Log("### InitMonster.moveToTarget ###");

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        boss.LookAt(InitMonster.Instance.target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * InitMonster.Instance.speedRun * multip);
    }

    /// <summary>
    /// �չ̻��� ����
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
    /// ����ü Quaternion ���ϴ� �Լ�.
    /// </summary>
    Quaternion getRotation(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        return Quaternion.LookRotation(direction);
    }
    
    /// <summary>
    /// ���Ϳ��� ���ظ� ���� �� ����ϴ� �޼ҵ�.
    /// </summary>
    /// <param name="amountOfDamage">���ط� ��ġ</param>
    public void onDamage(int amountOfDamage)
    {

        if (isInvincibility) {
            // Debug.Log("### ���� ���� �Դϴ�. ###");
            return; 
        }

        // Debug.Log(amountOfDamage);

        if (target == null)
        {
            settingTarget(GameObject.FindGameObjectWithTag("Player"));
        }

        showDamageText(amountOfDamage);
        monsterHp = monsterHp - amountOfDamage;

        // ���Ͱ� ���ظ� �ް� ������ ���� ó��
        if(monsterHp <= 0 && !isInvincibility)
        {
            startInvincibility();

            if (phaseState == 0) // 1������ ����
            {
                if (isDebugging)
                {
                    Debug.Log("### 1������ ����");
                }

                closePhase1();
                ActiveChangeInvoke();
            }
            else if (phaseState == 1) // 2������ ����
            {
                if (isDebugging)
                {
                    Debug.Log("### 2������ ����");
                }
                setPhaseState(2);
                closePhase2();
                ActiveChangeInvoke2();
            }
            else if (phaseState == 2) // 3������ ����
            {
                if (isDebugging)
                {
                    Debug.Log("### 3������ ����");
                }
                closePhase3();
            }
        }


    }

    public void changeForPhase1()
    {
        ActiveChangePhase2();
        setPhaseState(1);
    }


    /// <summary>
    /// ���ظ� ���� ��� ������ �ؽ�Ʈ ó���ϱ�
    /// </summary>
    /// <param name="damage"></param>

    public void showDamageText(int damage)
    {

        Transform hudPos = CameraController.instance.playerBox.transform;

        GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
        hudText.transform.SetParent(GameObject.Find("Canvas").transform);
        hudText.AddComponent<DamageText>();
        hudText.GetComponent<DamageText>().damage = damage; // ������ ����

    }

    /// <summary>
    /// ������ �����Ѵ�.
    /// </summary>
    public void startInvincibility()
    {
        if (isDebugging)
        {
            Debug.Log("### ���� ###");
        }
        isInvincibility = true;
    }

    /// <summary>
    /// ������ �����Ѵ�.
    /// </summary>
    public void closeInvincibility()
    {
        if (isDebugging)
        {
            Debug.Log("### ���� ���� ###");
        }
        isInvincibility = false;
    }


    // BossScript �� �ִ� ����ϴ� �Լ���
    public void ActiveChangeInvoke()
    {
        Invoke("changeForPhase1", 11f);
        if (haveMonsterAttack != null)
        {
            haveMonsterAttack.destroyAttack();
        }
    }

    public void ActiveChangeInvoke2()
    {
        Invoke("ActiveChangePhase3", 0.3f);
        if (haveMonsterAttack != null)
        {
            haveMonsterAttack.destroyAttack();
        }
    }

    void ActiveChangePhase2()
    {
        _PhaseOneBoss.gameObject.SetActive(false);
        _PhaseTwoBoss.gameObject.SetActive(true);
        _PhaseTwoModel.forward = _deadForward;
    }

    void ActiveChangePhase3()
    {
        _PhaseTwoBoss.gameObject.SetActive(false);

        _PhaseThreeBoss.gameObject.SetActive(true);
        _PhaseThreeModel.forward = _deadForward;
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
        if (phase == 2)
        {
            Debug.Log("ani changed - 2");
            ani = PhaseTwoModel.GetComponent<Animator>();
        }
        else if (phase == 3)
        {
            Debug.Log("ani changed - 3");
            ani = PhaseThreeModel.GetComponent<Animator>();
        }
    }
    /// <summary>
    /// 1������ ����
    /// </summary>
    public void closePhase1()
    {
        ani.SetInteger("aniInt", 6);
        ani.SetBool("DEATH", true);
    }

    /// <summary>
    /// 2������ ����
    /// </summary>
    public void closePhase2()
    {
        ani.SetInteger("aniInt", 5);
        ani.SetBool("DEATH", true);
    }

    /// <summary>
    /// 3������ ����
    /// </summary>
    public void closePhase3()
    {
        ani.SetInteger("aniInt", 9);
        ani.SetBool("DEATH", true);
        showChangeCamera2();
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
    // �������

    private Boolean flagSpeedDown = false;

    /// <summary>
    /// ���� �ӵ� ���� ����
    /// </summary>

    public void actionSpeedDown()
    {
        _prevSpeedRun = _speedRun;
        _speedRun -= (_speedRun * 0.3f);
        animator.SetFloat("SPEED", 0.7f);
        flagSpeedDown = true;

    }

    /// <summary>
    /// ���� �ӵ� ���� ����
    /// </summary>

    public void afterSpeedDown()
    {
        _speedRun = _prevSpeedRun;
        animator.SetFloat("SPEED", 1f);
        flagSpeedDown = false;
    }

    private Boolean _flagStun = false;
    
    public Boolean flagStun
    {
        get { return _flagStun; }
    }

    /// <summary>
    /// ���� ����
    /// </summary>

    public void actionStun() {

        _flagStun = true;
        animator.SetBool("STUN", true);
        animator.Play("Idle");

    }

    /// <summary>
    /// ���� ����
    /// </summary>

    public void afterStun()
    {
        animator.SetBool("STUN", false);
        _flagStun = false;
    }

    /// <summary>
    /// ���� �� ī�޶� ��ȯ
    /// </summary>
    public void showChangeCamera1()
    {
        Transform transformCamera = gameObject.transform.Find("Camera1");
        transformCamera.gameObject.SetActive(true);
        animator.SetFloat("SPEED", 0.8f);
        GameManager.instance.setStop();
    }

    /// <summary>
    /// ���� �� ī�޶� ��ȯ
    /// </summary>
    public void closeChangeCamera1()
    {
        Transform transformCamera = gameObject.transform.Find("Camera1");
        transformCamera.gameObject.SetActive(false);
        GameManager.instance.setStart();
    }

    /// <summary>
    /// ���� �� ī�޶� ��ȯ
    /// </summary>
    public void showChangeCamera2()
    {
        Transform transformCamera = gameObject.transform.Find("Camera2");
        transformCamera.gameObject.SetActive(true); 
        GameManager.instance.setStop();
    }

    /// <summary>
    /// ���� �� ī�޶� ��ȯ
    /// </summary>
    public void closeChangeCamera2()
    {
        Debug.Log("closeChangeCamera2");
        Transform transformCamera = gameObject.transform.Find("Camera2");
        transformCamera.gameObject.SetActive(false);
        GameManager.instance.setStart();
        EndingManager.instance.StartEvent();
    }


    public MonsterAttack haveMonsterAttack;

}
