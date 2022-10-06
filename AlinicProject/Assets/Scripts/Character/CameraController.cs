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
        Quaternion defaultAngle = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
        instance.transform.rotation = defaultAngle;
        instance.transform.parent = player.transform;
        instance.transform.localPosition = Vector3.zero;
    }
}
