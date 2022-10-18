using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffStatus : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject buffSrc;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject newBuff = Instantiate<GameObject>(buffSrc);
            newBuff.SetActive(true);
            newBuff.transform.SetParent(scrollRect.content);
        }
    }
}
