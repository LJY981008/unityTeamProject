using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    PositionTest pt;
    // Start is called before the first frame update
    void Start()
    {
        pt = FindObjectOfType<PositionTest>();
        Debug.Log(pt.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, pt.transform.position, Time.deltaTime * 2.0f);
    }
}
