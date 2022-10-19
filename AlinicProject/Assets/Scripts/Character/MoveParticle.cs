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
    public RaycastHit hit;
    float deleteTime;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(7, 7, true);
        speed = 30f;
        shotgunPos = Vector3.zero;
        saveSpeed = speed;
        playerBox = GameManager.instance.playerBox;
    }
    void OnEnable()
    {
        deleteTime = 0.0f;
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
        hit = PlayerUtill.GetShotEndPos();
        endPos = hit.point;
        endPos += shotgunPos;
        transform.LookAt(endPos);
    }

    void Update()
    {
        deleteTime += Time.deltaTime;
        if (deleteTime > 3.0f)
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
    private void OnTriggerEnter(Collider co)
    {
        //speed = 0;
        //ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Vector3 pos = hit.point;

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
        if (co.transform.root.CompareTag("Monster"))
        {
            InitMonster monster = co.GetComponent<Collider>().transform.root.gameObject.GetComponent<InitMonster>();
            monster.onDamage((int)GameManager.instance.CurrentDamage);
        }
        shotgunPos = Vector3.zero;
        ObjectPoolManager.ReturnBullet(this);
    }
}
