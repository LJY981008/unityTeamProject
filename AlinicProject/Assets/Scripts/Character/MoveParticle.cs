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
    private Color originColor;
    private Color slowColor;
    
    
    private void Awake()
    {
        Physics.IgnoreLayerCollision(7, 7, true);   // �Ѿ˳����� �浹 ����
        originColor = new Color(1, 1, 1);
        slowColor = new Color(0.06f, 0.6f, 1.0f);
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
            if (spawnParticle.currentBullet == "Ice")
            {
                if (!spawnParticle.isSlow)
                {
                    spawnParticle.isSlow = true;
                    StartCoroutine(MonsterSlow(co.gameObject, monster));
                }
            }
        }
        
        shotgunShotPos = Vector3.zero;
        spawnParticle.setReturnBullet(this);
    }
    IEnumerator MonsterSlow(GameObject obj, InitMonster monster)
    {
        // �ִϸ��̼ǵ� ���ο�����ǰ� �ۼ�
        monster.speedRun -= (float)(monster.speedRun * 0.3);
        SkinnedMeshRenderer skin = obj.GetComponent<SkinnedMeshRenderer>();
        while (spawnParticle.currentBullet == "Ice")
        {
            skin.materials[0].color = slowColor;
            yield return new WaitForSeconds(10);
        }
        spawnParticle.isSlow = false;
        skin.materials[0].color = originColor;
        monster.speedRun = 15f;
    }
}
