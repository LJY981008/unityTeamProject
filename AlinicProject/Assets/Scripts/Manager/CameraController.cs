using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Vector3 offset;
    public GameObject playerBox;
    public bool isDie = false;

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        offset = new Vector3(-0.1f, 1.7f, -0.1f);
        SettingCam(playerBox);
    }

    private void Update()
    {
        if (isDie)
        {
            if (transform.localRotation.y > -0.5f) 
                transform.Rotate(Vector3.down * Time.deltaTime * 30.0f);
        }
    }
    // 카메라 위치 설정
    public void SettingCam(GameObject player)
    {
        Quaternion l = Quaternion.Euler(new Vector3(0, 0, 0));
        instance.transform.localRotation = l;
        instance.transform.parent = player.transform;
        instance.transform.localPosition = offset;
    }
}
