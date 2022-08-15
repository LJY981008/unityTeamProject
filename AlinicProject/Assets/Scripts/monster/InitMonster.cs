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
        // 타겟이 없으면
        if (_target == null)
        {
            searchTarget();
        }

        // 타겟이 있으면...
        if (_target != null)
        {

        }
    }

    void searchTarget() // 주변 캐릭터 찾기
    {
        // 주변의 오브젝트 불러오기
        Collider[] colls = Physics.OverlapSphere(transform.position, 200.0f);

        // 주변에 오브젝트가 있으면..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];

            // 캐릭터가 맞으면, 타겟 설정
            if (tmpColl.gameObject.name.Equals(_target_name))
            {
                // 타겟 오브젝트 넣어주기
                _target = tmpColl.gameObject;
                break;
            }
        }
    }

    public float distanceToTarget() // 목표까지의 거리 구하기
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void moveToTarget() // 타겟으로 이동하기
    {
        // Debug.Log("### MonsterPhase1Run.OnStateUpdate ###");
        transform.LookAt(target.transform.position);
        // 타겟에 접근하기
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * move_speed);
    }

}
