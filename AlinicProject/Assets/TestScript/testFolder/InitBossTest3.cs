using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InitBossTest3 : MonoBehaviour
{
    InitBossTest2 ibt2;
    Transform _childT;
    // Start is called before the first frame update
    void Start()
    {
        ibt2 = FindObjectOfType<InitBossTest2>();
        _childT = transform.Find("2_Model");
    }

    // Update is called once per frame
    void Update()
    {
        if (ibt2 != null)
        {
            if (ibt2.triggerInt == 2)
            {
                _childT.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("null");
        }
    }
}
