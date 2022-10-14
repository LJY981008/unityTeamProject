using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Area : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(PlayerChar.instance.transform.position.x, 0, PlayerChar.instance.transform.position.z)
                ));
        }
    }
}
