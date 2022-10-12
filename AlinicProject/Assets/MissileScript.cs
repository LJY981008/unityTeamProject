using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject hitEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, cp.normal);
        Debug.Log(cp.point);
        if(hitEffect != null)
        {
            var Effect = Instantiate(hitEffect, cp.point, rot);
            /*var effectPart = Effect.GetComponent<ParticleSystem>();
            if(effectPart != null)
            {
                Destroy(effectPart, effectPart.main.duration);
            }*/
        }
        Destroy(gameObject);
    }
}
