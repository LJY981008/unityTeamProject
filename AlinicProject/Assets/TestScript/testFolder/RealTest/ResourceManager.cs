using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public List<Transform> childTs;
    public Transform childT;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        childTs = new List<Transform>();
        for (int i = 0; i < 3; i++)
        {
            childT = transform.GetChild(i);
            childTs.Add(childT);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
