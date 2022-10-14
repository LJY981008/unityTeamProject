using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class MonsterAttack
{

    private bool isDebugging = true;

    private GameObject _target;
    private string _targetName = "Player";
    private GameObject _attackArea;
    private GameObject _attackPoint;

    private int _damage;
    private float _area;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MonsterAttack(int damage, float area)
    {
        _damage = damage;
        _area = area;
        _target = InitMonster.Instance.target;
    }

    public void startAttack()
    {
        if(isDebugging) { Debug.Log("### MonsterAttack.startAttack ###"); }
        _attackArea = createAttackArea();
        _attackPoint = createAttackPoint();
    }

    public void endAttack()
    {
        if (isDebugging) { Debug.Log("### MonsterAttack.endAttack ###"); }

        Collider[] colls = Physics.OverlapSphere(_attackPoint.transform.position, _area);

        // 몬스터 주변에 오브젝트가 있으면..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];

            GameObject root = tmpColl.gameObject.transform.root.gameObject;

            // 캐릭터가 맞으면, 타겟 설정
            if (root.name.Equals(_targetName))
            {
                // 타겟 오브젝트에 넣어주기
                PlayerBody player = root.GetComponent<PlayerBody>();
                player.OnDamage(_damage);
            }
        }
        GameObject.Destroy(_attackArea);
        GameObject.Destroy(_attackPoint);
    }


    /// <summary>
    /// 공격 범위를 생성한다.
    /// </summary>
    public GameObject createAttackArea()
    {
        GameObject tmpAttackArea = Resources.Load("Monster/AttackArea") as GameObject;
        GameObject objAttackArea = GameObject.Instantiate(tmpAttackArea);
        objAttackArea.transform.position = new Vector3(_target.transform.position.x, 100, _target.transform.position.z);
        DecalProjector decalAttackArea = objAttackArea.GetComponent<DecalProjector>();
        decalAttackArea.size = new Vector3(_area * 2, _area * 2, 10000);

        return objAttackArea;
    }

    /// <summary>
    /// 공격 지점을 생성한다.
    /// </summary>
    public GameObject createAttackPoint()
    {
        GameObject tmpAttackPoint = new GameObject();
        GameObject objAttackPoint = GameObject.Instantiate(tmpAttackPoint);
        objAttackPoint.transform.position = _target.transform.position;

        return objAttackPoint;
    }

}
