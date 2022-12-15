using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiMissile : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 target;
    private Vector3 dir;
    private float speed;
    void Start()
    {
        speed = 20f;
        rigid = GetComponent<Rigidbody>();
        target = UltiSkill.instance._target;
        transform.position = new Vector3(target.x, 100, target.z);
        transform.LookAt(target);
        GetDir();
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain") || other.CompareTag("build"))
        {
            UltiSkill.instance.ArrivalMissile();
        }

    }
    private void GetDir()
    {
        dir = (target - transform.position).normalized;
    }
}
