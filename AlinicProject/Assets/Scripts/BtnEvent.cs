using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image btnImage;
    public Image panel;
    public Sprite push;
    public Sprite pool;
    Color src;
    float speed = 1f;
    private void Awake()
    {
        src = panel.color;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("´Ù¿î");
        btnImage.sprite = push;
    }
    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("¾÷");
        btnImage.sprite = pool;
        StartCoroutine(UpdateAlpha());
        

    }
    IEnumerator UpdateAlpha()
    {
        while (panel.color.a >= 0.1)
        {
            src.a -= Time.deltaTime * speed;
            panel.color = src;
            yield return new WaitForEndOfFrame();
        }
        panel.gameObject.SetActive(false);
        GameManager.instance.isStart = true;
    }
}
