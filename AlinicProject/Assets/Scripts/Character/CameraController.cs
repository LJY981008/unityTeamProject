using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
    }

    // 카메라 위치 설정
    public static void SettingCam(GameObject player)
    {
        ;
        Quaternion l = Quaternion.Euler(new Vector3(0, 180, 0));
        instance.transform.localRotation = l;
        instance.transform.parent = player.transform;
        instance.transform.localPosition = Vector3.zero;
    }
}
