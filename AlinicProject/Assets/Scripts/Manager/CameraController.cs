using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 카메라 세팅 스크립트
 * 
 */
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public GameObject playerBox;
    public bool isDie = false;

    private Vector3 offset;     // 카메라의 위치 보정
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        offset = new Vector3(-0.1f, 1.7f, 0.1f);
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
    // 카메라 위치 설정 함수
    public void SettingCam(GameObject player)
    {
        Quaternion l = Quaternion.Euler(new Vector3(0, 0, 0));
        instance.transform.localRotation = l;
        instance.transform.parent = player.transform;
        instance.transform.localPosition = offset;
    }
}
