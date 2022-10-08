using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 startPos;
    public float bulletSpeed = 5.0f;
    public Transform gun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            //ObjectPoolManager.ReturnBullet(this);
        }
        else
        {
            //ObjectPoolManager.ReturnBullet(this);
        }
    }
    private void OnEnable()
    {
        startPos = transform.position;
        gun = PlayerUpper.instance.transform;
        transform.forward = gun.forward;
    }

    /*void Update()
    {
        if (Vector3.Distance(startPos, transform.position) > 10)
        {
            Debug.Log("10¿Ãµø");
            //ObjectPoolManager.ReturnBullet(this);
        }
        else
        {
            transform.Translate(-(transform.forward * bulletSpeed * Time.deltaTime), Space.World);
        }
    }*/
    
    
}
