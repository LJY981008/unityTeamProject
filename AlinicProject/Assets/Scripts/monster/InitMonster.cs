using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InitMonster : MonoBehaviour
{

    private GameObject _target;
    private string _target_name = "Character";
    private float _move_speed;

    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }

    public float move_speed
    {
        get { return _move_speed; }
        set { _move_speed = value; }
    }

    private void Awake()
    {
        _move_speed = 15.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ���� ������
        if (_target == null)
        {
            searchTarget();
        }

        // Ÿ���� ������...
        if (_target != null)
        {

        }
    }

    void searchTarget() // �ֺ� ĳ���� ã��
    {
        // �ֺ��� ������Ʈ �ҷ�����
        Collider[] colls = Physics.OverlapSphere(transform.position, 200.0f);

        // �ֺ��� ������Ʈ�� ������..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];

            // ĳ���Ͱ� ������, Ÿ�� ����
            if (tmpColl.gameObject.name.Equals(_target_name))
            {
                // Ÿ�� ������Ʈ �־��ֱ�
                _target = tmpColl.gameObject;
                break;
            }
        }
    }

    public float distanceToTarget() // ��ǥ������ �Ÿ� ���ϱ�
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void moveToTarget() // Ÿ������ �̵��ϱ�
    {
        // Debug.Log("### MonsterPhase1Run.OnStateUpdate ###");
        transform.LookAt(target.transform.position);
        // Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * move_speed);
    }

}
