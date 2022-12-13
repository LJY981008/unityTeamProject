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
    private float speed;
    public GameObject effectToSpawn;
    private UltiMissile ultiMissile;
    private void Awake()
    {
        instance = this;
        speed = 20f;
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
}
