using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/**
 * UI ��Ʈ�� �Ŵ���
 * 
 */
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Canvas mainCanvas;               // �÷��� ȭ�� ĵ����           
    public TextMeshProUGUI textMaxAmmo;     // �ִ� ź ���� text
    public TextMeshProUGUI textCurrentAmmo; // ���� ź ���� text
    public TextMeshProUGUI textBossName;    // ���� �̸� text
    public TextMeshProUGUI textBuffDuration;// ���� ���ӽð� text
    public List<Image> imageWeapons;        // ��밡���� ���� �̹��� ����Ʈ
    public Image imageHp;                   // �÷��̾� ü�� ������
    public Image imageBuffPanel;            // ���� ĭ �̹���
    public Image imageBuffBody;             // ������ ���� ������
    public Image imageBuffIcon;             // ������ ȿ�� ������
    public Image imageBossHpFill;           // ������ ü�� ������
    public Image imageBossHpBackground;     // ������ ü�� ��
    public Image imageDamageEffect;         // ������ ����Ʈ
    public Image btnStart;
    public Image imageSkillPanel;           // ��ų ������
    public Image imagePistolSkillEffect;
    public Image imageUltiIcon;
    public SpawnParticle spawnParticle;
    public int skillCoolTime;
    public int ultiCoolTime;
    private float buffDuration;             // ���� ���ӽð�
    private float buffTime;                 // ���� �����ð�
    private float saveBossHpAmount;         // ���� ü���� amount ����
    private float updateHpSpeed;            // �÷��̾� ü�� �������� �پ��� �ӵ�
    private float damageEffectSpeed;        // ������ ����Ʈ ���� �ӵ�
    private float damageDestAlpha;          // ������ ����Ʈ �ִ� alpha ��
    private float disPlayerToBos;           // ���� ü���� ǥ���ϱ� ���� �÷��̾�� ���� ������ �Ÿ�
    private bool isDamage;                  // ���� ������ üũ
    private bool damageEffectTrigger;       // ������ ����Ʈ ��Ÿ���� �������� ���� Ʈ����
    private bool isSkill;
    private bool isUlti;
    public bool isDe;
    private Image selectWeapon;             // ������ ���� �̹���
    private Vector3 bodyGunImagePos;        // ���� ������ ������ ��ġ
    private Vector3 bodyMagazineImagePos;   // ���� ������ ȿ���� ��ġ
    private Color colorDamageSrc;           // ������ ����Ʈ �� ����
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
    // �÷��̾� ü�� ���� �Լ�
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

    // ������ �� ǥ�� �Լ�
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
    // ȹ���� ���� ǥ�� �Լ�
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
    // ȹ���� ���� ������ ���� �Լ�
    public void SetBuffIcon(string icon, int duration)
    {
        buffDuration = duration;
        imageBuffIcon.sprite = ResourcesManager.instance.GetBuffIconSprite(icon);
        imageBuffPanel.gameObject.SetActive(true);
    }
    // ȹ���� ���� ���ӽð� ǥ�� �Լ�
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
    // ���� ü�� ���� �Լ�
    public void UpdateBossHp(int currentHp, int maxHp)
    {
        float amount = ((float)currentHp / (float)maxHp);   
        if (imageBossHpFill.fillAmount > amount)
        {
            imageBossHpFill.fillAmount -= Time.deltaTime * 0.1f;
        }
        saveBossHpAmount = amount;
    }
    // ���� HP ���� �Լ�
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
    // �÷��̾��� ���� ����Ʈ �Լ�
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
    // ������ �Ծ��� �� �Լ�
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
