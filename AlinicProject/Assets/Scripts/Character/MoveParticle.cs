using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticle : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public PlayerBody playerBox;
    private float saveSpeed;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 shotgunPos;
    private void Awake()
    {
        speed = 100f;
        shotgunPos = Vector3.zero;
        saveSpeed = speed;
        playerBox = GameManager.instance.playerBox;
    }
    void OnEnable()
    {
        startPos = transform.position;
        speed = saveSpeed;
        if (muzzlePrefab != null && shotgunPos == Vector3.zero)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = playerBox.transform.forward;
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
        endPos = PlayerUtill.GetShotEndPos();
        endPos += shotgunPos;
        transform.LookAt(endPos);
    }

    void Update()
    {
        if(Vector3.Distance(startPos, transform.position) > 100)
        {
            ObjectPoolManager.ReturnBullet(this);
        }
        if (speed != 0)
        {
            if (endPos == Vector3.zero) transform.position += playerBox.transform.forward * (speed * Time.deltaTime);
            else transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
            
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    void OnCollisionEnter(Collision co)
    {
        Debug.Log(co.transform.tag + ":" + co.transform.root.tag);
        if (!co.transform.root.CompareTag("Player"))
        {
            speed = 0;
            ContactPoint contact = co.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }
            if (co.collider.transform.root.CompareTag("Monster"))
            {
                InitMonster monster = co.collider.transform.root.gameObject.GetComponent<InitMonster>();
                monster.onDamage((int)GameManager.instance.CurrentDamage);
            }
            shotgunPos = Vector3.zero;
            ObjectPoolManager.ReturnBullet(this);
        }
    }
}
