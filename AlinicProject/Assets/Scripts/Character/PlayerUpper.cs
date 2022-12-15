using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 플레이어 무기 스크립트
 * 
 */
public class PlayerUpper : MonoBehaviour
{
    public GunData gunData;                 // 총의 스크럽터블 스크립트

    private AudioSource audioSource;
    private Animator animator;
    private Transform muzzlePivot;          // 총구 위치 오브젝트
    private SpawnParticle spawnParticle;    // 탄 스폰 스크립트
    private Vector3 spawnPos;               // 무기 스폰 위치
    private Quaternion spawnRotate;         // 무기 스폰 방향
    
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        spawnParticle = GameManager.instance.GetComponent<SpawnParticle>();

        spawnRotate = Quaternion.Euler(new Vector3(0, 180, 0));
        spawnPos = new Vector3(0.0f, 1.6f, 0.0f);
        muzzlePivot = FindFireSpot(transform, "Muzzle Pivot");
        gunData.currentAmmo = gunData.maxAmmo;
        gunData.currentCoolTime = 0;
    }
    private void OnEnable()
    {
        if(gunData.currentCoolTime != 0)
        {
            float currentProgress = (gunData.skillCoolTime - gunData.currentCoolTime) / gunData.skillCoolTime;
            UIManager.instance.imageSkillPanel.fillAmount = currentProgress;
            UIManager.instance.skillProgressAmount = (float)1 / gunData.skillCoolTime * Time.deltaTime;
            UIManager.instance.isSkill = true;
        }
        else
        {
            UIManager.instance.imageSkillPanel.fillAmount = 1.0f;
        }
        if (UIManager.instance != null)
        {
            UIManager.instance.textMaxAmmo.text = gunData.maxAmmo.ToString();
            UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
            UIManager.instance.SelectWeaponActive(transform.name.Replace("(Clone)", ""));
        }
        transform.localPosition = spawnPos;
        transform.localRotation = spawnRotate;
        audioSource.playOnAwake = false;
        spawnParticle.firePoint = muzzlePivot.gameObject;
        GameManager.instance.currentDamage = gunData.damage;
        GameManager.instance.prevDamage = gunData.damage;
    }
    private void Update()
    {
        if (gunData.currentAmmo < 1) animator.SetBool("isFire", false);
    }

    // 격발 함수
    public void FireGun(int index)
    {
        if (!animator.GetBool("isRun"))
        {
            if (gunData.currentAmmo > 0)
            {
                animator.SetBool("isFire", true);
                if (index == 2)
                {

                }
            }
            else
            {
                GameManager.instance.shotCount = 0;
                // 총알 없을 때 격발시 오디오 현재 이상해서 수정해야함
                /* AudioClip audioClip = AudioManager.instance.GetDryClip(transform.name.Replace("(Clone)", ""));
                 Debug.Log(audioClip.name);
                 PlayAudio(audioClip);*/
            }
        }
        else
        {
            GameManager.instance.shotCount = 0;
        }
    }   
    public void FireSkill()
    {
        if (!animator.GetBool("isRun"))
        {
            animator.Play("Auto Fire");
        }
    }
    // 대기 함수
    public void IdleGun()
    {
        animator.SetBool("isFire", false);
    }
    // 장전 함수
    public void ReloadGun(int index)
    {
        if (!animator.GetBool("isRun"))
        {
            // 탄약이 가득차있을 때는 작동안하는 이벤트 작성
            if (gunData.currentAmmo < gunData.maxAmmo)
            {
                animator.SetBool("isReload", true);
            }
            else
            {
                Debug.Log("탄이 가득참");
            }
        }
    }
    // 탄 감소 함수
    public void UseAmmo()
    {
        gunData.currentAmmo--;
        UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
    }
    // 탄 장전 함수
    public void ReloadAmmo()
    {
        gunData.currentAmmo = gunData.maxAmmo;
        UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
    }
    // 오디오 재생 함수
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    // 격발 이벤트 함수
    public void shot()
    {
        spawnParticle.SetEffect();
        spawnParticle.firePoint = muzzlePivot.gameObject;
        if (!spawnParticle.currentBullet.Equals("Grenade"))
            UseAmmo();
    }
    // 총구 위치 찾는 함수
    public Transform FindFireSpot(Transform _t, string name)
    {
        if (_t.name.Equals(name))
            return _t;
        for (int i = 0; i < _t.childCount; i++)
        {
            Transform findTr = FindFireSpot(_t.GetChild(i), name);
            if (findTr != null)
                return findTr;
        }
        return null;
    }
    // 이동 트리거 함수
    public void IsMove(bool trigger)
    {
        animator.SetBool("isMove", trigger);
    }
    // 달리기 트리거 함수
    public void IsRun(bool trigger)
    {
        animator.SetBool("isRun", trigger);
    }
    // 사망 트리거 함수
    public void IsDie()
    {
        animator.SetBool("isDie", true);
    }
}
