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
    public static PlayerUpper instance;

    public int[] maxAmmo = { 30, 12, 12 };
    public int[] ammo;

    private Animator animator;
    public AudioSource audioSource;
    List<AudioSource> reloadAudioSource;
    private void Awake()
    {
        if (instance == null) instance = this;
        ammo = new int[3];
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        
        Debug.Log(clip.name + "클립명");
    }
}
