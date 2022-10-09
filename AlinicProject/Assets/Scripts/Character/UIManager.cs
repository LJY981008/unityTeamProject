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
    // 0,1 라이플 2,3 피스톨 4,5 샷건
    public List<Image> imageWeapons;
    Image selectWeapon;


    private void Awake()
    {
        instance = this;
    }
    public static void UpdateHpState(float max, float current)
    {
        instance.imageHp.fillAmount = current / max;
    }

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

}
