using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtill : MonoBehaviour
{
    public static PlayerUtill instance; 
    private float rotateY = 0.0f;
    private float rotateX = 0.0f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
    }
    public void MoveRotate(float rotateSizeX, float rotateSizeY)
    {
        rotateY += rotateSizeY;
        rotateX += rotateSizeX;
        rotateX = Mathf.Clamp(rotateX, -40, 40);
        Quaternion playerQuat = Quaternion.Euler(new Vector3(rotateX, rotateY, 0.0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * 500f);

    }
}
