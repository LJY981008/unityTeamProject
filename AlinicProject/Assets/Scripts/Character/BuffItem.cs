using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 버프아이템 이벤트 스크립트
 */
public class BuffItem : MonoBehaviour
{
    float rotateSpeed = 50f;
    float boundSpeed = 0.3f;
    float boundMinLocation = 1.3f;
    float boundMaxLocation = 1.7f;
    Vector3 boundDirection;
    private void OnEnable()
    {
        boundDirection = Vector3.up;
    }

    int rInt;

    private void Awake()
    {
        // Debug.Log("BuffItem.Awake : " + rInt);

        changeBuff();
    }

    //
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        if (transform.position.y < boundMinLocation) boundDirection = Vector3.up;
        else if (transform.position.y > boundMaxLocation) boundDirection = Vector3.down;
        transform.Translate(boundDirection * boundSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {

            // Debug.Log("BuffItem.OnTriggerEnter : " + rInt);

            if (UIManager.instance.imageBuffPanel.gameObject.activeSelf == true)
            {
                UIManager.instance.OffPanel();
            }
            UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
            BuffStatus.instance.RandomBuffBody(rInt);
            BuffStatus.instance.RandomBuffIcon(rInt);
            // UIManager.instance.SetBuffBody("Magazine"); // Magazine or Gun
            // UIManager.instance.SetBuffIcon("Large");    // 
            GameManager.instance.isSpawnItem = false;

            changeBuff();

            ObjectPoolManager.ReturnItem(this);
            
        }
    }

    private void changeBuff()
    {

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();

        rInt = Random.Range(1, 8);

        // Debug.Log("BuffItem.changeBuff : " + rInt);

        /*
         * 빨 : 화상탄
         * 주 : 대용량 탄창
         * 노 : 경량화 탄창
         * 초 : 부식탄
         * 파 : 빙결탄
         * 남 : 무력탄
         * 보 : 과냉각
         * 흰 : 응급처치 탄창
         */

        switch (rInt)
        {
            case 1: // 부식탄
                mr.material.color = Color.green;
                break;
            case 2: // 화염탄
                mr.material.color = Color.red;
                break;
            case 3: // 무력탄
                mr.material.color = new Color(102, 0, 204); // 보라색
                break;
            case 4: // 빙결탄
                mr.material.color = new Color(0, 0, 255); ;
                break;
            case 5: // 응급치료탄
                mr.material.color = Color.white;
                break;
            case 6: // 과냉각탄
                mr.material.color = new Color(0, 0, 51); // 남색
                break;
            case 7: // 대용량탄창
                mr.material.color = new Color(255, 128, 0); // 주황색
                break;
            default: // 경량화탄창
                mr.material.color = new Color(255, 255, 0); // 노랑색
                break;
        }
    }
}
