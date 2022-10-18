using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveTestScript : MonoBehaviour
{
    public static playerMoveTestScript instance;
    public bool IsTalk;
    public GameObject NPC;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        IsTalk = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TalkWithNPC();
        if (!IsTalk)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, 0.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(0.5f, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0, 0, 0.5f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.5f, 0, 0);
            }
        }
    }

    public void TalkWithNPC()
    {
        if(Vector3.Distance(transform.position,NPC.transform.position) <= 10.0f && 
            Input.GetKeyDown(KeyCode.Space))
        {
            IsTalk = true;

        }
    }

}
