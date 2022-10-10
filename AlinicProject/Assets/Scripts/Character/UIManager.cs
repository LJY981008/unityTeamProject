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
    public Image imageHp;
    public List<Image> imageWeapons;
    public Image imageBuffPanel;
    public Image imageBuffBody;
    public Image imageBuffIcon;
    public TextMeshProUGUI textBuffDuration;

    private float buffDuration;
    private Image selectWeapon;
    private Vector3 bodyGunImagePos;
    private Vector3 bodyMagazineImagePos;
    private float time;
    private void Awake()
    {
        instance = this;
        buffDuration = -1;
        time = 0;
        bodyGunImagePos = new Vector3(7.0f, 0.0f, 0.0f);
        bodyMagazineImagePos = new Vector3(7.0f, 35.0f, 0.0f);
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
    }
    // 체력 증감 UI 적용
    public void UpdateHpState(float max, float current)
    {
        imageHp.fillAmount = current / max;
    }

    // 선택한 총 표시
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

}
