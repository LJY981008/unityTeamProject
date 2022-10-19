using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMgr : MonoBehaviour
{
    public static BuffMgr instance;
    void Awake() 
    {
        instance = this;
    }
    public GameObject buffPerfabs;

    public void CreateBuff(string type,float per, float du, Sprite icon)
    {
        GameObject go = Instantiate(buffPerfabs, transform);
        go.GetComponent<BuffBase>().Init(type,per,du);
        go.GetComponent<UnityEngine.UI.Image>().sprite = icon;
    }

}
