using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class MonsterAttack
{



    private bool isDebugging = false;

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

        InitMonster.Instance.haveMonsterAttack = this;

    }

    private Boolean isDestroy = false;

    public void startAttack()
    {
        if(isDebugging) { Debug.Log("### MonsterAttack.startAttack ###"); }
        _attackArea = createAttackArea();
        _attackPoint = createAttackPoint();
    }

    public void endAttack()
    {
        if (isDestroy) return;
        if (isDebugging) { Debug.Log("### MonsterAttack.endAttack ###"); }
        


        // 스턴 상태가 아닐 경우
        if (InitMonster.Instance.flagStun == false)
        {

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

        }
        InitMonster.Instance.haveMonsterAttack = null;
        GameObject.Destroy(_attackArea);
        GameObject.Destroy(_attackPoint);
    }

    public void startAttackArea()
    {
        if (isDebugging) { Debug.Log("### MonsterAttack.startAttack ###"); }
        _attackArea = createAttackArea(InitMonster.Instance.gameObject);
    }

    public void destroyAttack()
    {
        isDestroy = true;
        InitMonster.Instance.haveMonsterAttack = null;
        if(_attackPoint != null)
        {
            GameObject.Destroy(_attackPoint);
        }
        GameObject.Destroy(_attackArea);
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
    /// 공격 범위를 생성한다.
    /// </summary>
    public GameObject createAttackArea(GameObject gObj)
    {
        GameObject tmpAttackArea = Resources.Load("Monster/AttackArea") as GameObject;
        GameObject objAttackArea = GameObject.Instantiate(tmpAttackArea);
        objAttackArea.transform.position = new Vector3(gObj.transform.position.x, 100, gObj.transform.position.z);
        Area classArea = objAttackArea.GetComponent<Area>();
        classArea.targetObject = gObj;

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
