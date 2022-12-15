using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �÷��̾��� �ֻ��� ������Ʈ
 */
public class PlayerBody : MonoBehaviour
{
    public float maxHP;             // �ִ� ü��
    public float currentHP;         // ���� ü��
    public Vector3 nuckbackDir;
    public Vector3 nuckbackDest;

    private float dieRotateSpeed;   // �׾��� �� ȸ�� �ӵ�
    private float destDieRotate;    // �׾��� �� ���� �ִ�ġ
    private Quaternion playerQuat;  // �׾��� �� ������ ����
    private void Awake()
    {
        dieRotateSpeed = 0f;
        destDieRotate = -90f;
        playerQuat = Quaternion.Euler(new Vector3(destDieRotate, 0.0f, 0.0f));
    }
    private void Update()
    {
        UIManager.instance.UpdateHpState(maxHP, currentHP);
        if (currentHP < 0.01) GameManager.instance.Die();
        if (dieRotateSpeed > 0) 
        {
            // �׾��� �� manfull�� �ִϸ��̼� �������·� ���ֱ� 
            transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * dieRotateSpeed);
            // ������ �ڷ� ������ �� ī�޶󿡵� ȸ�� ����
            if(transform.rotation.x < -0.4f)
                CameraController.instance.isDie = true;
        }
    }
    // �÷��̾��� ���� �Լ�
    public void OnDamage(float damege)
    {
        UIManager.instance.OnDamage();
        currentHP -= damege;
    }
    // ���� ������ ȹ��
    private void OnTriggerExit(Collider other)
    {
        // ���� �±׷� �����ϱ�
        if (other.transform.CompareTag("Player"))
        {
            UIManager.instance.imageBuffPanel.gameObject.SetActive(false);
        }
    }
    public IEnumerator Heal()
    {
        int cnt = 0;
        while (cnt <= 10)
        {
            cnt++;
            currentHP += 2;
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    public void DoCoroutine(string name)
    {
        StartCoroutine(name);
    }
    public void NuckBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, nuckbackDest, 20f * Time.deltaTime);
    }
    // �׾��� �� ȸ�� �ӵ� �Լ�
    public void DieRotate()
    {
        dieRotateSpeed = 2.0f;
    }
}
