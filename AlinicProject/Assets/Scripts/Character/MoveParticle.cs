using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 총알 스크립트
 * 
 */
public class MoveParticle : MonoBehaviour
{
    
    public GameObject muzzlePrefab;         // 총구
    public GameObject hitPrefab;            // 적중 시의 이펙트 프리팹
    public PlayerBody playerBox;            // 플레이어 본체
    public Vector3 shotgunShotPos;          // 샷건 멀티샷의 목표지점 보정값
    public SpawnParticle spawnParticle;
    private float durationTime;             // 총알의 유지 시간
    private float bulletSpeed;              // 총알의 속도
    private float saveSpeed;                // 속도 저장
    private Vector3 destShotPos;            // 총알의 목표 지점
    private RaycastHit hit;                 // 레이캐스트 값 저장
    public float additionalDamage;
    
    
    
    private void Awake()
    {
        Physics.IgnoreLayerCollision(7, 7, true);   // 총알끼리의 충돌 제외
        Physics.IgnoreLayerCollision(0, 0, true);
        bulletSpeed = 50f;
        additionalDamage = 1.0f;
        shotgunShotPos = Vector3.zero;
        saveSpeed = bulletSpeed;
        playerBox = GameManager.instance.playerBody;
    }
    void OnEnable()
    {
        durationTime = 0.0f;
        bulletSpeed = saveSpeed;
        if (muzzlePrefab != null && shotgunShotPos == Vector3.zero)
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
        hit = PlayerUtill.GetShotEndPos();  // 레이캐스트 호출
        destShotPos = hit.point;            // 목표점 레이캐스트 충돌위치
        destShotPos += shotgunShotPos;      // 샷건 탄환 보정
        transform.LookAt(destShotPos);      // 총알의 방향 보정
    }

    void Update()
    {
        if (bulletSpeed <= 150)
            bulletSpeed += 10;
        durationTime += Time.deltaTime;
        if (durationTime > 3.0f)
        {
            spawnParticle.setReturnBullet(this);
        }
        if (bulletSpeed != 0)
        {
            // 목표지점이 존재하면 목표지점으로 이동 이외에는 바라보는 방향으로 이동
            if (destShotPos != Vector3.zero)
                transform.position = Vector3.MoveTowards(transform.position, destShotPos, Time.deltaTime * bulletSpeed);
            else 
                transform.position += playerBox.transform.forward * (bulletSpeed * Time.deltaTime);

        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    // 탄알 충돌 이벤트
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
            monster.onDamage((int)(GameManager.instance.currentDamage * GameManager.instance.additionalDamage) + GameManager.instance.plusDamage);
            if(spawnParticle.currentBullet == "Normal")
            {

            }
            else if (spawnParticle.currentBullet == "Ice")
            {
                ApplyBuff.instance.isDamage = true;
                if (!ApplyBuff.instance.isSlow)
                {
                    ApplyBuff.instance.isSlow = true;
                    ApplyBuff.instance.DoCoroutine("MonsterSlow");
                }
            }
            else if (spawnParticle.currentBullet == "Fire")
            {
                ApplyBuff.instance.SetBurnHitCount();
                if ((!ApplyBuff.instance.isBurn) && (ApplyBuff.instance.HitBurnCount == 3))
                {
                    ApplyBuff.instance.isBurn = true;
                    ApplyBuff.instance.DoCoroutine("MonsterBurn");
                }
            }
        }
        shotgunShotPos = Vector3.zero;
        spawnParticle.setReturnBullet(this);
    }
    
}
