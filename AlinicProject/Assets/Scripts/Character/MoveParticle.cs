using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �Ѿ� ��ũ��Ʈ
 * 
 */
public class MoveParticle : MonoBehaviour
{
    
    public GameObject muzzlePrefab;         // �ѱ�
    public GameObject hitPrefab;            // ���� ���� ����Ʈ ������
    public PlayerBody playerBox;            // �÷��̾� ��ü
    public Vector3 shotgunShotPos;          // ���� ��Ƽ���� ��ǥ���� ������
    public SpawnParticle spawnParticle;
    private float durationTime;             // �Ѿ��� ���� �ð�
    private float bulletSpeed;              // �Ѿ��� �ӵ�
    private float saveSpeed;                // �ӵ� ����
    private Vector3 destShotPos;            // �Ѿ��� ��ǥ ����
    private RaycastHit hit;                 // ����ĳ��Ʈ �� ����
    public float additionalDamage;
    
    
    
    private void Awake()
    {
        Physics.IgnoreLayerCollision(7, 7, true);   // �Ѿ˳����� �浹 ����
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
        hit = PlayerUtill.GetShotEndPos();  // ����ĳ��Ʈ ȣ��
        destShotPos = hit.point;            // ��ǥ�� ����ĳ��Ʈ �浹��ġ
        destShotPos += shotgunShotPos;      // ���� źȯ ����
        transform.LookAt(destShotPos);      // �Ѿ��� ���� ����
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
            // ��ǥ������ �����ϸ� ��ǥ�������� �̵� �̿ܿ��� �ٶ󺸴� �������� �̵�
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
    // ź�� �浹 �̺�Ʈ
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
