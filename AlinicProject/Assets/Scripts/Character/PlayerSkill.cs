using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill instance;
    public SpawnParticle spawnParticle;
    public PlayerUpper playerbleWeapon;
    public Animator animator;
    private int skillCoolTime;
    private float pistolDuration;
    private float pistolSkillCoefficient;
    private float shotgunDuration;
    private int gunIndex;
    public float prevDamage;
    public string prevBullet;
    public int plusDamage;

    public int SkillCoolTime
    {
        set 
        {
            // 0 라이플, 1 피스톨, 2 샷건
            if (value == 0)
            {
                gunIndex = value;
                skillCoolTime = 10;
            }
            else if (value == 1)
            {
                skillCoolTime = 15;
            }
            else if (value == 2)
            {
                skillCoolTime = 10;
            }
            gunIndex = value;
        }
        get { return skillCoolTime; }
    }
    private void Awake()
    {
        instance = this;
        gunIndex = -1;
        skillCoolTime = -1;
        pistolDuration = 5.0f;
        pistolSkillCoefficient = 1.5f;
        shotgunDuration = 5.0f;
        plusDamage = 4;
    }

    public void Skill()
    {
        if (gunIndex == 0)
        {
            UIManager.instance.skillCoolTime = skillCoolTime;
            playerbleWeapon = GameManager.instance.playableWeapon;
            SkillRifle();

        }
        else if (gunIndex == 1)
        {
            UIManager.instance.skillCoolTime = skillCoolTime;
            StartCoroutine(SkillPistol());
        }
        else if(gunIndex == 2)
        {
            UIManager.instance.skillCoolTime = skillCoolTime;
            playerbleWeapon = GameManager.instance.playableWeapon;
            animator = playerbleWeapon.GetComponent<Animator>();
            StartCoroutine(SkillShotgun());
        }
    }

    IEnumerator SkillShotgun()
    {
        float prevSpeed = animator.GetFloat("AttackSpeed");
        animator.SetFloat("AttackSpeed", prevSpeed * 1.3f);
        GameManager.instance.plusDamage += plusDamage;
        yield return new WaitForSecondsRealtime(shotgunDuration);
        animator.SetFloat("AttackSpeed", prevSpeed);
        GameManager.instance.plusDamage -= plusDamage;
    }

    // 잔탄발사 버그가 너무 많음
    /*IEnumerator SkillShotgun()
    {
        while (playerbleWeapon.gunData.currentAmmo > 0)
        {
            GameManager.instance.plusDamage = 4;
            playerbleWeapon.FireGun(gunIndex);
            yield return new WaitForSecondsRealtime(0.1f);
            animator.SetBool("Idle", true);
            animator.Play("Idle");
        }
        GameManager.instance.plusDamage = 0;
        GameManager.instance.isSkill = false;
    }*/
    void SkillRifle()
    {
        prevBullet = spawnParticle.currentBullet;
        prevDamage = GameManager.instance.currentDamage;
        spawnParticle.currentBullet = "Grenade";
        GameManager.instance.currentDamage = 60f;
        
        playerbleWeapon.FireSkill();

    }
    IEnumerator SkillPistol()
    {
        float prevSpeed = GameManager.instance.plusSpeed;
        GameManager.instance.plusSpeed = pistolSkillCoefficient;
        UIManager.instance.imagePistolSkillEffect.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(pistolDuration);
        UIManager.instance.imagePistolSkillEffect.gameObject.SetActive(false);
        GameManager.instance.plusSpeed = prevSpeed;
    }

}
