using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill instance;
    private int skillCoolTime;
    private float pistolDuration;
    private float pistolSkillCoefficient;
    private int gunIndex;

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
    }

    public void Skill()
    {
        if (gunIndex == 0)
        {

        }
        else if (gunIndex == 1)
        {
            UIManager.instance.skillCoolTime = skillCoolTime;
            StartCoroutine(SkillPistol());
        }
        else if(gunIndex == 2)
        {

        }
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
