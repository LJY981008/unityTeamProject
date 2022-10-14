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
/// ������ ���� �������� �ڵ带 ��� Ŭ����
/// </summary>

public class InitMonster : MonoBehaviour
{

    private static InitMonster instance = null;
    System.Random random;

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

    /// <summary>
    /// true : Ÿ���� �����Ѵ�.
    /// false : �������� �ʴ´�.
    /// </summary>
    bool isSettingTarget = true;

    /// <summary>
    /// true : ����
    /// fasle : ���� ����
    /// </summary>
    bool isInvincibility = true;


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
    private string _targetName = "Character";

    // Run ��ǿ����� �ӵ�
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

        // �ʱ� ������ ����
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

        if(isInvincibility) { return; }

        if(target == null)
        {
            settingTarget(GameObject.FindGameObjectWithTag("Character"));
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
                setPhaseState(1);
                BossScript.instance.closePhase1();
            }
            else if (phaseState == 1) // 1������ ����
            {
                if (isDebugging)
                {
                    Debug.Log("### 2������ ����");
                }
                setPhaseState(2);
                BossScript.instance.closePhase2();
            }
            else if (phaseState == 2) // 1������ ����
            {
                if (isDebugging)
                {
                    Debug.Log("### 3������ ����");
                }
                BossScript.instance.closePhase3();
            }
        }


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
        hudText.transform.localPosition = new Vector3(200 + random.Next(100), 80, -30);
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

}
