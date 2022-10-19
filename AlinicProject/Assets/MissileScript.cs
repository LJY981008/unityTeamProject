using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject hitEffect;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, cp.normal);
        
        if (!collision.collider.CompareTag("Monster"))
        {
            if (hitEffect != null)
            {
                var Effect = Instantiate(hitEffect, cp.point, rot);
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(collision.collider.gameObject.name);
        }
            
    }
}
