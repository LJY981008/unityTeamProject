using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

/// <summary>
/// ������ ���� �������� �ڵ带 ��� Ŭ����
/// </summary>

public class InitMonster : MonoBehaviour
{

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
    }

    /// <summary>
    /// ���� ����� �����Ѵ�.
    /// </summary>
    /// <param name="state">������</param>
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
    /// ����� ü���� ��� �迭 ����
    /// </summary>
    private static int[] PHASE_HP = { 10000, 20000, 30000 };

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

        // �ʱ� ������ ����
        setPhaseState(0);

        // �̻��� ����Ʈ ����
        effectToSpawn = Effects[0];
    }

    // Update is called once per frame
    void Update()
    {
        // �̻��� ������ ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackHandRazer();
        }

        // ������ Ÿ���� ������
        if (_target == null)
        {
            searchTarget();
        }

        // ������ Ÿ���� ������...
        if (_target != null)
        {

        }
    }

    /// <summary>
    /// ���� �ֺ� ĳ���� ã��
    /// </summary>
    void searchTarget()
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
                _target = tmpColl.gameObject;
                break;
            }
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

        //TODO: �չ̻����� ������ ��ġ : 3������ bone046 ó������� ��
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
        monsterHp = monsterHp - amountOfDamage;

        //TODO: ���Ͱ� ���ظ� �ް� ������ ���� ó��

    }

}
