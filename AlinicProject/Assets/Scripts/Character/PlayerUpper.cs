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
    private Animator animator;
    private int[] maxAmmo = { 30, 12, 12 };
    private float fireDistance = 50f; // 사정거리
    private Transform muzzlePivot;   // 총구 피봇 오브젝트
    private Vector3 firePos;        //총구 위치
    private float rotateY = 0.0f;
    private float rotateX = 0.0f;

    private void Awake()
    {
        if (instance == null) instance = this;
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        CameraController.SettingCam(gameObject);
        audioSource.playOnAwake = false;
        ammo = new int[3];
        muzzlePivot = FindFireSpot(transform, "Muzzle Pivot");
        muzzleState = muzzlePivot.GetComponent<MuzzleState>();

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
        firePos = muzzlePivot.position;
        GameObject ammo;
        Bullet bullet;
        //RaycastHit hit;
        //Vector3 hitPos = Vector3.zero;

        //StartCoroutine(ShotEffect(hitPos));
        ObjectPoolManager.GetBullet(transform ,firePos);
        UseAmmo(index);
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
    public static void MoveRotate(float rotateSizeX, float rotateSizeY)
    {
        instance.rotateY += rotateSizeY;
        instance.rotateX += -rotateSizeX;
        instance.rotateX = Mathf.Clamp(instance.rotateX, -40, 40);
        Quaternion playerQuat = Quaternion.Euler(new Vector3(instance.rotateX, instance.rotateY, 0.0f));
        instance.transform.rotation = Quaternion.Slerp(instance.transform.rotation, playerQuat, Time.deltaTime * 500f);
        
    }
}
