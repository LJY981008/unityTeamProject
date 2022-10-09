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
    private void Awake()
    {
        instance = this;
    }
    public static void UpdateHpState(float max, float current)
    {
        instance.imageHp.fillAmount = current / max;
    }
}
