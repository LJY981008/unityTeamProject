using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public float maxHP = 1000;
    public float currentHP = 1000;
    float rotateSpeed = 0f;
    float endRotate = -90f;
    Quaternion playerQuat;
    Quaternion cameraQuat;
    private void Awake()
    {
        playerQuat = Quaternion.Euler(new Vector3(endRotate, 0.0f, 0.0f));
        cameraQuat = Quaternion.Euler(new Vector3(0.0f, endRotate, 0.0f));
    }
    private void Update()
    {
        UIManager.instance.UpdateHpState(maxHP, currentHP);
        if (currentHP < 0.01) GameManager.instance.Die();
        if (rotateSpeed > 0) 
        {
            // �׾��� �� manfull�� �ִϸ��̼� �������·� ���ֱ� 
            transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * rotateSpeed);
            // ������ �ڷ� ������ �� ī�޶󿡵� ȸ�� ����
            if(transform.rotation.x < -0.4f)
                CameraController.instance.isDie = true;
        }
    }
    public void OnDamage(float damege)
    {
        UIManager.instance.OnDamage();
        currentHP -= damege;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            UIManager.instance.imageBuffPanel.gameObject.SetActive(false);
        }
    }
    public void DieRotate()
    {
        rotateSpeed = 2.0f;
    }
}
