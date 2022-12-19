using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerEffect : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;

    void Start()
    {
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * 3 * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();


            if (psHit != null)
            {

                // Debug.Log("���� Ÿ�� : " + psHit.gameObject.transform.root.tag);
                Debug.Log("���� Ÿ�� : " + collision.gameObject.transform.root.name);
                if (collision.gameObject.transform.root.tag == "Player")
                {
                    PlayerBody player = collision.gameObject.transform.root.GetComponent<PlayerBody>();
                    player.OnDamage(30);
                }

                Destroy(hitVFX, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }

            //InitMonster monster = collision.gameObject.GetComponent<InitMonster>();
            //monster.onDamage(50);
        }
        Destroy(gameObject);
    }

}