using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/**
 * UI 컨트롤 매니저
 * 
 */
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Canvas mainCanvas;               // 플레이 화면 캔버스           
    public TextMeshProUGUI textMaxAmmo;     // 최대 탄 개수 text
    public TextMeshProUGUI textCurrentAmmo; // 현재 탄 개수 text
    public TextMeshProUGUI textBossName;    // 보스 이름 text
    public TextMeshProUGUI textBuffDuration;// 버프 지속시간 text
    public List<Image> imageWeapons;        // 사용가능한 무기 이미지 리스트
    public Image imageHp;                   // 플레이어 체력 게이지
    public Image imageBuffPanel;            // 버프 칸 이미지
    public Image imageBuffBody;             // 버프의 종류 아이콘
    public Image imageBuffIcon;             // 버프의 효과 아이콘
    public Image imageBossHpFill;           // 보스의 체력 게이지
    public Image imageBossHpBackground;     // 보스의 체력 바
    public Image imageDamageEffect;         // 데미지 이펙트
    public Image btnStart;
    public Image imageSkillPanel;           // 스킬 아이콘
    public Image imagePistolSkillEffect;
    public Image imageUltiIcon;
    public SpawnParticle spawnParticle;
    public int skillCoolTime;
    public int ultiCoolTime;
    private float buffDuration;             // 버프 지속시간
    private float buffTime;                 // 버프 유지시간
    private float saveBossHpAmount;         // 보스 체력의 amount 저장
    private float updateHpSpeed;            // 플레이어 체력 게이지가 줄어드는 속도
    private float damageEffectSpeed;        // 데미지 이펙트 출현 속도
    private float damageDestAlpha;          // 데미지 이펙트 최대 alpha 값
    private float disPlayerToBos;           // 보스 체력을 표시하기 위한 플레이어와 보스 사이의 거리
    private bool isDamage;                  // 입은 데미지 체크
    private bool damageEffectTrigger;       // 데미지 이펙트 나타나고 지워지는 기준 트리거
    private bool isSkill;
    private bool isUlti;
    public bool isDe;
    private Image selectWeapon;             // 선택한 무기 이미지
    private Vector3 bodyGunImagePos;        // 버프 아이콘 종류의 위치
    private Vector3 bodyMagazineImagePos;   // 버프 아이콘 효과의 위치
    private Color colorDamageSrc;           // 데미지 이펙트 값 저장
    private float skillProgressAmount;
    private float ultiProgressAmount;
    public delegate void De();
    public De de;
    private void Awake()
    {
        instance = this;
        skillCoolTime = 0;
        buffTime = 0.0f;
        buffDuration = -1.0f;
        updateHpSpeed = 0.3f;
        saveBossHpAmount = 1.0f;
        damageDestAlpha = 0.1f;
        damageEffectSpeed = 0.4f;
        disPlayerToBos = 50f;
        skillProgressAmount = 0f;
        ultiProgressAmount = 0f;
        ultiCoolTime = 30;

        bodyGunImagePos = new Vector3(7.0f, 0.0f, 0.0f);
        bodyMagazineImagePos = new Vector3(7.0f, 35.0f, 0.0f);
        colorDamageSrc = imageDamageEffect.color;

        isUlti = false;
        isSkill = false;
        isDamage = false;
        isDe = false;
        damageEffectTrigger = false;
    }
    private void Update()
    {
        if(buffDuration > -1) {
            SetBuffDuration();
        }
        else if (buffDuration == -1)
        {
            isDe = true;
        }
        if (isDamage)
        {
            DamageEffect();
        }
        if (isDe)
        {
            isDe = false;
            de = OffPanel;
            de();
        }
        if (isSkill)
        {
            imageSkillPanel.fillAmount += skillProgressAmount;
            if(imageSkillPanel.fillAmount >= 1)
            {
                isSkill = false;
            }
        }
        if (isUlti)
        {
            imageUltiIcon.fillAmount += ultiProgressAmount;
            if(imageUltiIcon.fillAmount >= 1)
            {
                isUlti = false;
            }
        }
    }
    public void OffPanel()
    {
        imageBuffPanel.gameObject.SetActive(false);
        if (spawnParticle.currentBullet == "Corrosion")
        {
            GameManager.instance.additionalDamage = 1.0f;
        }
        spawnParticle.currentBullet = "Normal";
        if(de != null) 
            de -= OffPanel;
    }
    // 플레이어 체력 갱신 함수
    public void UpdateHpState(float max, float current)
    {
        if(Math.Round(imageHp.fillAmount, 2) > Math.Round(current / max, 2))
        {
            imageHp.fillAmount -= Time.deltaTime * updateHpSpeed;
        }
        else if (Math.Round(imageHp.fillAmount, 2) < Math.Round(current / max, 2))
        {
            imageHp.fillAmount += Time.deltaTime * updateHpSpeed;
        }
    }

    // 선택한 총 표시 함수
    public void SelectWeaponActive(string gunName)
    {
        if(selectWeapon != null)
        {
            imageWeapons.Add(selectWeapon);
        }
        selectWeapon = imageWeapons.Find(o => o.name.Contains(gunName));
        selectWeapon.gameObject.SetActive(true);
        imageWeapons.Remove(selectWeapon);
        foreach (var image in imageWeapons)
        {
            image.gameObject.SetActive(false);
        }
    }
    // 획득한 버프 표시 함수
    public void SetBuffBody(string body)
    {
        imageBuffBody.sprite = ResourcesManager.instance.GetBuffBodySprite(body);
        if (body.Equals("Magazine"))
        {
            imageBuffIcon.rectTransform.anchoredPosition = bodyMagazineImagePos;
        }
        else
        {
            imageBuffIcon.rectTransform.anchoredPosition = bodyGunImagePos;
        }
    }
    // 획득한 버프 아이콘 세팅 함수
    public void SetBuffIcon(string icon, int duration)
    {
        buffDuration = duration;
        imageBuffIcon.sprite = ResourcesManager.instance.GetBuffIconSprite(icon);
        imageBuffPanel.gameObject.SetActive(true);
    }
    // 획득한 버프 지속시간 표시 함수
    public void SetBuffDuration()
    {
        textBuffDuration.text = buffDuration.ToString();
        buffTime += Time.deltaTime;
        if (buffTime >= 1)
        {
            buffDuration--;
            buffTime = 0;
        }
    }
    // 보스 체력 갱신 함수
    public void UpdateBossHp(int currentHp, int maxHp)
    {
        float amount = ((float)currentHp / (float)maxHp);   
        if (imageBossHpFill.fillAmount > amount)
        {
            imageBossHpFill.fillAmount -= Time.deltaTime * 0.1f;
        }
        saveBossHpAmount = amount;
    }
    // 보스 HP 출현 함수
    public void SetEnableBossHp(float dis)
    {
        if (dis < disPlayerToBos)
        {
            textBossName.gameObject.SetActive(true);
            imageBossHpFill.fillAmount = saveBossHpAmount;
        }
        else if (dis >= disPlayerToBos)
        {
            textBossName.gameObject.SetActive(false);
            saveBossHpAmount = imageBossHpFill.fillAmount;
        }           
    }
    // 플레이어의 피해 이펙트 함수
    public void DamageEffect()
    {
        if (damageEffectTrigger) 
        {
            damageEffectSpeed *= -1;
            damageEffectTrigger = false;
        }
        colorDamageSrc.a += Time.deltaTime * damageEffectSpeed;
        imageDamageEffect.color = colorDamageSrc;
        if (imageDamageEffect.color.a >= damageDestAlpha)
        {
            damageEffectTrigger = true;
        }
        if (imageDamageEffect.color.a <= 0)
        {
            colorDamageSrc.a = 0.0f;
            damageEffectSpeed *= -1;
            imageDamageEffect.color = colorDamageSrc;
            isDamage = false;
        }
    }
    // 데미지 입었을 때 함수
    public void OnDamage()
    {
        isDamage = true;
    }
    public void SkillEvent()
    {
        skillProgressAmount = (float)1 / skillCoolTime * Time.deltaTime;
        imageSkillPanel.fillAmount = 0;
        isSkill = true;
    }
    public void UltiEvent()
    {
        ultiProgressAmount = (float)1 / ultiCoolTime * Time.deltaTime;
        imageUltiIcon.fillAmount = 0;
        isUlti = true;
    }
}
