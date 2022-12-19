using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Area : MonoBehaviour
{

    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject != null)
        {
            gameObject.transform.position = new Vector3(targetObject.transform.position.x, 100, targetObject.transform.position.z);
        }
    }
}
