using UnityEngine;

public class InitMonsterTest : MonoBehaviour
{
    // ��ǥ���� ���� ����
    private GameObject _target;

    // ��ǥ���� Name
    private string _targetName = "Player";

    // Run ��ǿ����� �ӵ�
    private float _speedRun;

    int _randomInt;
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

    float _latelyCastSkillTime;
    public float latelyCastSkillTime
    {
        get { return _latelyCastSkillTime; }
        set { _latelyCastSkillTime = value; }
    }

    private void Awake()
    {
        _speedRun = 10.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ÿ���� �������� �˻��Լ� ����
        if (_target == null)
        {
            //Debug.Log("searching...");
            searchTarget();
        }
        //Ÿ���� �����Ҷ� 
        else if (_target != null)
        {
            //Debug.Log("player detected");
            //moveToTarget();
        }
    }
    public void searchTarget()
    {

        //Debug.Log("searchTarget()");
        // ���� �ֺ��� ������Ʈ �ҷ�����
        Collider[] colls = Physics.OverlapSphere(transform.position, 300.0f);
        // ���� �ֺ��� ������Ʈ�� ������..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];
            // ĳ���Ͱ� ������, Ÿ�� ����
            if (tmpColl.gameObject.name.Equals(_targetName))
            {
                // Ÿ�� ������Ʈ�� �־��ֱ�
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

        // ���Ͱ� Ÿ���� �ٶ󺸱�
        transform.LookAt(target.transform.position);
        // ���͸� Ÿ�ٿ� �����ϱ�
        transform.position =
        Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speedRun);
    }

    public float initLatelyTime()
    {
        // ��ų ��� �ð� �ʱ�ȭ
        return latelyCastSkillTime = Time.time;
    }
    public float getDistanceOfTime(float a, float b)
    {
        return a - b;
    }
    public void castSkill()
    {
        Debug.Log("skill casted");
    }
}
