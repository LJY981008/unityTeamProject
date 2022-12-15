using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 플레이어의 최상위 오브젝트
 */
public class PlayerBody : MonoBehaviour
{
    public float maxHP;             // 최대 체력
    public float currentHP;         // 현재 체력
    public Vector3 nuckbackDir;
    public Vector3 nuckbackDest;

    private float dieRotateSpeed;   // 죽었을 때 회전 속도
    private float destDieRotate;    // 죽었을 때 눞는 최대치
    private Quaternion playerQuat;  // 죽었을 때 적용할 각도
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
            // 죽었을 때 manfull에 애니메이션 십자형태로 해주기 
            transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * dieRotateSpeed);
            // 일정량 뒤로 누웠을 때 카메라에도 회전 적용
            if(transform.rotation.x < -0.4f)
                CameraController.instance.isDie = true;
        }
    }
    // 플레이어의 피해 함수
    public void OnDamage(float damege)
    {
        UIManager.instance.OnDamage();
        currentHP -= damege;
    }
    // 버프 아이템 획득
    private void OnTriggerExit(Collider other)
    {
        // 버프 태그로 변경하기
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
    // 죽었을 때 회전 속도 함수
    public void DieRotate()
    {
        dieRotateSpeed = 2.0f;
    }
}
