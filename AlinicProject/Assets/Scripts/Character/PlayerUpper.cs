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
    [HideInInspector] public static PlayerUpper instance;
    [HideInInspector] public int[] ammo;
    public GunData gunData;

    private MuzzleState muzzleState;        //이펙트 정보
    private AudioSource audioSource;        // 사운드
    private LineRenderer bulletLineRenderer; // 총알의 궤적
    private Animator animator;
    private int[] maxAmmo = { 30, 12, 12 };
    private float fireDistance = 50f; // 사정거리
    private GameObject muzzlePivot;   // 총구 피봇 오브젝트
    private Vector3 firePos;        //총구 위치

    private void Awake()
    {
        if (instance == null) instance = this;
        muzzlePivot = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();
        bulletLineRenderer = gameObject.AddComponent<LineRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        muzzleState = muzzlePivot.GetComponent<MuzzleState>();

        audioSource.playOnAwake = false;
        bulletLineRenderer.positionCount = 2;   // 궤적에 사용할 점 갯수, 총구위치와 총알이 닿은 위치
        bulletLineRenderer.enabled = false;
        ammo = new int[3];

    }
    private void Update()
    {
        
    }


    // 발사
    public void FireGun(int index)
    {
        animator.SetBool("isFire", true);
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
        if (ammo[index] < maxAmmo[index])
        {
            animator.SetBool("isReload", true);
        }
        else
        {
            Debug.Log("탄이 가득참 : " + ammo);
        }
    }
    // 탄감소
    public void UseAmmo(int index)
    {
        ammo[index]--;
        Debug.Log("잔탄 : " + ammo[index]);
    }
    // 탄 장전
    public void ReloadAmmo(int index)
    {
        ammo[index] = maxAmmo[index];
        Debug.Log("장전완료" + ammo[index]);
    }
    // 오디오 재생
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        
        Debug.Log(clip.name + "클립명");
    }
    public void shot(int index)
    {
        firePos = muzzlePivot.transform.position;
        RaycastHit hit;
        Vector3 hitPos = Vector3.zero;

        if(Physics.Raycast(firePos, muzzlePivot.transform.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if(target != null)
            {
                Debug.Log("맞음");
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            else
            {
                Debug.Log("안맞음");
            }
            hitPos = hit.point;
        }
        else
        {
            Debug.Log("엘스");
            hitPos = muzzlePivot.transform.position + muzzlePivot.transform.forward * fireDistance;
        }
        StartCoroutine(ShotEffect(hitPos));
        UseAmmo(index);
    }
    // 이펙트
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 이펙트 발생 호출 작성

        // 총구 위치 저장
        firePos = transform.localPosition + muzzleState.GetMuzzlePos();
        // 선의 시작은 총구
        bulletLineRenderer.SetPosition(0, firePos);
        // 선의 끝은 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);
        // 활성화하여 궤적을 그림
        bulletLineRenderer.enabled = true;

        // 처리대기
        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }
}
