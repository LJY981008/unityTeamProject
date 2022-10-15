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
    private void Awake()
    {
        speed = 100f;
        saveSpeed = speed;
        playerBox = GameManager.instance.playerBox;
    }
    void OnEnable()
    {
        startPos = transform.position;
        speed = saveSpeed;
        if (muzzlePrefab != null)
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
    }

    void Update()
    {
        if(Vector3.Distance(startPos, transform.position) > 100)
        {
            ObjectPoolManager.ReturnBullet(this);
        }
        if (speed != 0)
        {
            //if (endPos == Vector3.zero) transform.position += playerBox.transform.forward * (speed * Time.deltaTime);
            //else transform.position += endPos.normalized * (speed * Time.deltaTime);
            transform.position += playerBox.transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    void OnCollisionEnter(Collision co)
    {
        if (!co.collider.CompareTag("Player"))
        {
            Debug.Log(co.collider.tag);
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
            if (co.collider.CompareTag("Monster"))
            {
                InitMonster monster = co.collider.gameObject.GetComponent<InitMonster>();
                monster.onDamage((int)GameManager.instance.CurrentDamage);
            }
            ObjectPoolManager.ReturnBullet(this);
        }
    }
}
