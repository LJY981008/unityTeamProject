using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class BtnEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image btnImage;
    public Image panel;
    public TextMeshProUGUI text;
    public Sprite push;
    public Sprite pool;
    Color tmpBtn;
    Color tmpText;
    Color tmpPanel;
    float speed = 1f;
    private void Awake()
    {
        tmpText = text.color;
        tmpPanel = panel.color;
        tmpBtn = btnImage.color;
    }
    public void OnPointerDown(PointerEventData data)
    {
        btnImage.sprite = push;
    }
    public void OnPointerUp(PointerEventData data)
    {
        btnImage.sprite = pool;
        StartCoroutine(UpdateAlpha());
    }
    IEnumerator UpdateAlpha()
    {
        while (panel.color.a >= 0.1)
        {
            tmpPanel.a -= Time.deltaTime * speed;
            tmpBtn.a -= Time.deltaTime * speed;
            tmpText.a -= Time.deltaTime * speed;
            panel.color = tmpPanel;
            text.color = tmpText;
            btnImage.color = tmpBtn;
            yield return new WaitForEndOfFrame();
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panel.gameObject.SetActive(false);
        GameManager.instance.isStart = true;
    }
}
