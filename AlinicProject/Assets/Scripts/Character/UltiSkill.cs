using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UltiSkill : MonoBehaviour
{
    public static UltiSkill instance;
    public Vector3 _target = Vector3.zero;
    private float _area = 5;
    private GameObject _attackArea;
    private GameObject _attackPoint;
    private GameObject missile;
    public GameObject effectToSpawn;
    private int missileDamage;
    //private UltiMissile ultiMissile;
    private void Awake()
    {
        instance = this;
        missileDamage = 200;
    }
    public void ArrivalMissile()
    {
        GameObject.Destroy(_attackArea);
        GameObject.Destroy(_attackPoint);
        GameObject.Destroy(missile);
        SpawnVFX();
    }
    public void StartUltiArea()
    {
        _target = PlayerUtill.GetShotEndPos().point;
        if (_target != Vector3.zero) {
            _attackArea = createAttackArea();
            _attackPoint = createAttackPoint();
            missile = Instantiate<GameObject>(ResourcesManager.instance.missile);
        }
    }
    public GameObject createAttackArea()
    {

        GameObject tmpAttackArea = Resources.Load("Monster/AttackArea") as GameObject;
        GameObject objAttackArea = GameObject.Instantiate(tmpAttackArea);
        objAttackArea.transform.position = new Vector3(_target.x, 100, _target.z);
        DecalProjector decalAttackArea = objAttackArea.GetComponent<DecalProjector>();
        decalAttackArea.size = new Vector3(_area * 2, _area * 2, 10000);

        return objAttackArea;
    }
    public GameObject createAttackPoint()
    {
        GameObject tmpAttackPoint = new GameObject();
        GameObject objAttackPoint = GameObject.Instantiate(tmpAttackPoint);
        objAttackPoint.transform.position = _target;

        return objAttackPoint;
    }
    public void SpawnVFX()
    {
        var vfx = Instantiate(effectToSpawn, _target, Quaternion.identity);
        var ps = GetFirstPS(vfx);
        MissileHit();
        Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax + 1);
    }
    public ParticleSystem GetFirstPS(GameObject vfx)
    {
        var ps = vfx.GetComponent<ParticleSystem>();
        if (ps == null && vfx.transform.childCount > 0)
        {
            foreach (Transform t in vfx.transform)
            {
                ps = t.GetComponent<ParticleSystem>();
                if (ps != null)
                    return ps;
            }
        }
        return ps;
    }
    public void MissileHit()
    {
        Collider[] colls = Physics.OverlapSphere(_attackPoint.transform.position, _area);
        // 몬스터 주변에 오브젝트가 있으면..
        foreach (var hit in colls)
        {
            GameObject root = hit.gameObject.transform.root.gameObject;
            Debug.Log(root.tag + " " + root.name);
            // 캐릭터가 맞으면, 타겟 설정
            if (root.CompareTag("Monster"))
            {
                // 타겟 오브젝트에 넣어주기
                InitMonster.Instance.onDamage(missileDamage);

            }
        }
    }
}
