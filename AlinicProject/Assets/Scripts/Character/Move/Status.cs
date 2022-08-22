using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour 
{
    public static Status instance;
    [Header("Walk, Run Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    private void Awake() 
    {
        if (instance == null) instance = this;
    }
}