using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffStatus : MonoBehaviour
{
    public static BuffStatus instance;
    public List<BuffBase> onBuff = new List<BuffBase>();
    public ScrollRect scrollRect;
    public GameObject buffSrc;
    int random;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    public float Buff(string type, float origin)
    {
        if(onBuff.Count > 0)
        {
            float tmep = 0f;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if(onBuff[i].type.Equals(type))
                    tmep += origin * onBuff[i].percentage;
            }
            return origin * tmep;
        }
        else
        {
            return origin;
        }
    }
    public void RandomBuffBody()
    {
        random = Random.Range(1,2);
        switch (random)
        {
            case 1:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Magazine");
                break;

            case 2:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Gun");
                break;
        }
    }
    public void RandomBuffIcon()
    {
        random = Random.Range(1,7);
        switch (random)
        {
            case 1:
                UIManager.instance.SetBuffIcon("Corrosion");
                break;
            case 2:
                UIManager.instance.SetBuffIcon("Fire");
                break;
            case 3:
                UIManager.instance.SetBuffIcon("Force");
                break;
            case 4:
                UIManager.instance.SetBuffIcon("Heal");
                break;
            case 5:
                UIManager.instance.SetBuffIcon("Ice");
                break;
            case 6:
                UIManager.instance.SetBuffIcon("Large");
                break;
            case 7:
                UIManager.instance.SetBuffIcon("Light");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject newBuff = Instantiate<GameObject>(buffSrc);
            newBuff.SetActive(true);
            newBuff.transform.SetParent(scrollRect.content);
        }
    }
}
