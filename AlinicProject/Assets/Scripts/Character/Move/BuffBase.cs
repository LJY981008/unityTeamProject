using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffBase : MonoBehaviour
{
    public string type;
    public float percentage, duration, currentTime;
    public Image icon;


    void Awake() 
    {
        icon = GetComponent<Image>();
    }
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    public void Init(string type,float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;

        Execute();

    }
    public void Execute()
    {
        BuffStatus.instance.onBuff.Add(this);
        BuffStatus.instance.RandomBuffBody();
        BuffStatus.instance.RandomBuffIcon();
        StartCoroutine(Activation());
    }
    IEnumerator Activation()
    {
        while (currentTime > 0)
        {
            currentTime -=0.1f; //
            yield return seconds;
        }
        currentTime = 0;
        DeActivation();
    }
    public void DeActivation()
    {
        BuffStatus.instance.onBuff.Remove(this);
        BuffStatus.instance.RandomBuffBody();
        BuffStatus.instance.RandomBuffIcon();
        Destroy(gameObject);
    }
}
