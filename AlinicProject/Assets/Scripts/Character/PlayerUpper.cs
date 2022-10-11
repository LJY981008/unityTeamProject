using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : �÷��̾� ��ü ��ũ��Ʈ
 */
public class PlayerUpper : MonoBehaviour
{
    public GunData gunData;

    private AudioSource audioSource;        // 사운드
    private Animator animator;
    private float fireDistance = 50f; // 사정거리
    private Transform muzzlePivot;   // 총구 피봇 오브젝트
    private Vector3 firePos;        //총구 위치
   
    private Quaternion l = Quaternion.Euler(new Vector3(0, 180, 0));
    private SpawnParticle spawnParticle;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        muzzlePivot = FindFireSpot(transform, "Muzzle Pivot");
        spawnParticle = GameManager.instance.GetComponent<SpawnParticle>();
        gunData.currentAmmo = gunData.maxAmmo;
    }
    private void OnEnable()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.textMaxAmmo.text = gunData.maxAmmo.ToString();
            UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
            UIManager.instance.SelectWeaponActive(transform.name.Replace("(Clone)", ""));
        }
        transform.localPosition = Vector3.zero;
        transform.localRotation = l;
        audioSource.playOnAwake = false;
        spawnParticle.firePoint = muzzlePivot.gameObject;
        GameManager.instance.CurrentDamage = gunData.damage;
    }
    // 발사
    public void FireGun(int index)
    {
        if (gunData.currentAmmo > 0)
        {
            animator.SetBool("isFire", true);
        }
        else
        {
            Debug.Log("탄없음");
        }
    }   
    // 대기, 
    public void IdleGun()
    {
        animator.SetBool("isFire", false);
    }
    // 장전
    public void ReloadGun(int index)
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
    // 탄감소
    public void UseAmmo()
    {
        gunData.currentAmmo--;
        UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
    }
    // 탄 장전
    public void ReloadAmmo()
    {
        gunData.currentAmmo = gunData.maxAmmo;
        UIManager.instance.textCurrentAmmo.text = gunData.currentAmmo.ToString();
    }
    // 오디오 재생
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void shot()
    {
        firePos = muzzlePivot.position;
        spawnParticle.firePoint = muzzlePivot.gameObject;
        spawnParticle.SetEffect();
        UseAmmo();
    }

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
    //이거 박스로 옮기기
   
}
