using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticle : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public GameObject playerBox;
    private float saveSpeed;
    public Vector3 startPos;
    private Vector3 thisFoward;
    private Vector3 focusCenter;
    private Vector3 shotEnd;
    private void Awake()
    {
        saveSpeed = speed;
        playerBox = Ex_GameManager.instance.playerBox;
    }
    private void Start()
    {
        
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
    }

    void Update()
    {
        //focusCenter = new Vector3(Camera.main.pixelHeight / 2, Camera.main.pixelWidth / 2);
        
        if(Vector3.Distance(startPos, transform.position) > 50)
        {
            ObjectPoolManager.ReturnBullet(this);
        }
        if (speed != 0)
        {
            transform.position += playerBox.transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    void OnCollisionEnter(Collision co)
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
        ObjectPoolManager.ReturnBullet(this);
    }
}
