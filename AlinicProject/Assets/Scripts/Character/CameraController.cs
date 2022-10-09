using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Vector3 offset;
    public GameObject playerBox;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SettingCam(playerBox);
    }

    void Update()
    {
    }

    // 카메라 위치 설정
    public void SettingCam(GameObject player)
    {
        Quaternion l = Quaternion.Euler(new Vector3(0, 0, 0));
        instance.transform.localRotation = l;
        instance.transform.parent = player.transform;
        instance.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.2f);
    }
}
