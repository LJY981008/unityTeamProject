using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Canvas canvas;
    public TextMeshProUGUI textMaxAmmo;
    public TextMeshProUGUI textCurrentAmmo;
    public TextMeshProUGUI textBossName;
    public Image imageHp;
    public List<Image> imageWeapons;
    public Image imageBuffPanel;
    public Image imageBuffBody;
    public Image imageBuffIcon;
    public TextMeshProUGUI textBuffDuration;
    public Image bossHpFill;
    public Image bossHpBackground;
    public Image imageDamageEffect;

    private float buffDuration;
    private Image selectWeapon;
    private Vector3 bodyGunImagePos;
    private Vector3 bodyMagazineImagePos;
    private float time;
    private float saveBossHpAmount;
    private float updateHpSpeed;
    private bool isDamage;
    private bool effectTrigger;
    private float elapsed;
    private float destAlpha;
    private Color colorDamageSrc;
    private void Awake()
    {
        instance = this;
        buffDuration = -1;
        time = 0;
        saveBossHpAmount = 1.0f;
        updateHpSpeed = 0.3f;
        bodyGunImagePos = new Vector3(7.0f, 0.0f, 0.0f);
        bodyMagazineImagePos = new Vector3(7.0f, 35.0f, 0.0f);
        colorDamageSrc = imageDamageEffect.color;
        elapsed = 0.4f;
        destAlpha = 0.1f;
        isDamage = false;
        effectTrigger = false;
    }
    private void Update()
    {
        if(buffDuration > -1) {
            SetBuffDuration();
        }
        else
        {
            imageBuffPanel.gameObject.SetActive(false);
        }
        if (isDamage)
        {
            DamageEffect();
        }
    }
    // ü�� ���� UI ����
    public void UpdateHpState(float max, float current)
    {
        if(imageHp.fillAmount > current / max)
        {
            imageHp.fillAmount -= Time.deltaTime * updateHpSpeed;
        }
    }

    // ������ �� ǥ��
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

    public void SetBuffBody(string body)
    {
        buffDuration = 20;
        imageBuffPanel.gameObject.SetActive(true);
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
    public void SetBuffIcon(string icon)
    {
        imageBuffIcon.sprite = ResourcesManager.instance.GetBuffIconSprite(icon);
    }
    public void SetBuffDuration()
    {
        textBuffDuration.text = buffDuration.ToString();
        time += Time.deltaTime;
        if (time >= 1)
        {
            buffDuration--;
            time = 0;
        }
    }
    public void UpdateBossHp(int currentHp, int maxHp)
    {
        float amount = ((float)currentHp / (float)maxHp);   
        if (bossHpFill.fillAmount > amount)
        {
            bossHpFill.fillAmount -= Time.deltaTime * 0.1f;
        }
        saveBossHpAmount = amount;
    }
    public void SetEnableBossHp(float dis)
    {
        if (dis < 50f)
        {
            textBossName.gameObject.SetActive(true);
            bossHpFill.fillAmount = saveBossHpAmount;
        }
        else if (dis >= 50f)
        {
            textBossName.gameObject.SetActive(false);
            saveBossHpAmount = bossHpFill.fillAmount;
        }           
    }
    public void DamageEffect()
    {
        if (effectTrigger) 
        {
            elapsed *= -1;
            effectTrigger = false;
        }
        colorDamageSrc.a += Time.deltaTime * elapsed;
        imageDamageEffect.color = colorDamageSrc;
        if (imageDamageEffect.color.a >= destAlpha)
        {
            effectTrigger = true;
        }
        if (imageDamageEffect.color.a <= 0)
        {
            colorDamageSrc.a = 0.0f;
            elapsed *= -1;
            imageDamageEffect.color = colorDamageSrc;
            isDamage = false;
        }
    }
    public void OnDamage()
    {
        isDamage = true;
    }
}
