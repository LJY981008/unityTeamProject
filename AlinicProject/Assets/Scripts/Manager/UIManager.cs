using System.Collections;
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

    private float buffDuration;             // ���� ���ӽð�
    private float buffTime;                 // ���� �����ð�
    private float saveBossHpAmount;         // ���� ü���� amount ����
    private float updateHpSpeed;            // �÷��̾� ü�� �������� �پ��� �ӵ�
    private float damageEffectSpeed;        // ������ ����Ʈ ���� �ӵ�
    private float damageDestAlpha;          // ������ ����Ʈ �ִ� alpha ��
    private float disPlayerToBos;           // ���� ü���� ǥ���ϱ� ���� �÷��̾�� ���� ������ �Ÿ�
    private bool isDamage;                  // ���� ������ üũ
    private bool damageEffectTrigger;       // ������ ����Ʈ ��Ÿ���� �������� ���� Ʈ����
    private Image selectWeapon;             // ������ ���� �̹���
    private Vector3 bodyGunImagePos;        // ���� ������ ������ ��ġ
    private Vector3 bodyMagazineImagePos;   // ���� ������ ȿ���� ��ġ
    private Color colorDamageSrc;           // ������ ����Ʈ �� ����

    private void Awake()
    {
        instance = this;
        
        buffTime = 0.0f;
        buffDuration = -1.0f;
        updateHpSpeed = 0.3f;
        saveBossHpAmount = 1.0f;
        damageDestAlpha = 0.1f;
        damageEffectSpeed = 0.4f;
        disPlayerToBos = 50f;

        bodyGunImagePos = new Vector3(7.0f, 0.0f, 0.0f);
        bodyMagazineImagePos = new Vector3(7.0f, 35.0f, 0.0f);
        colorDamageSrc = imageDamageEffect.color;

        isDamage = false;
        damageEffectTrigger = false;
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
    // �÷��̾� ü�� ���� �Լ�
    public void UpdateHpState(float max, float current)
    {
        if(imageHp.fillAmount > current / max)
        {
            imageHp.fillAmount -= Time.deltaTime * updateHpSpeed;
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
    // ȹ���� ���� ������ ���� �Լ�
    public void SetBuffIcon(string icon)
    {
        imageBuffIcon.sprite = ResourcesManager.instance.GetBuffIconSprite(icon);
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
}
