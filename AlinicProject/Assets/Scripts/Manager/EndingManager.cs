using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndingManager : MonoBehaviour
{
    public static EndingManager instance;
    public TextMeshProUGUI text;
    Image panel;
    Color tmpPanel;
    Color zero;
    Color tmpText;

    private void Awake()
    {
        instance = this;
        panel = transform.gameObject.GetComponent<Image>();
        tmpPanel = panel.color;
        zero = tmpPanel;
        zero.a = 0;
        panel.color = zero;

        tmpText = text.color;
        zero = tmpText;
        zero.a = 0;
        text.color = zero;
    }
    private void OnEnable()
    {
        StartCoroutine(UpdateAlpha());
    }
    IEnumerator UpdateAlpha()
    {
        while (panel.color.a <= 1)
        {
            tmpPanel.a += Time.deltaTime * 1f;
            tmpText.a += Time.deltaTime * 1f;
            panel.color = tmpPanel;
            text.color = tmpText;
            yield return new WaitForEndOfFrame();
        }
    }
}
