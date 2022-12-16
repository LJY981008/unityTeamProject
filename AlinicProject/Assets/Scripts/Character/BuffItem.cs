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
    float boundMinLocation = 0.2f;
    float boundMaxLocation = 0.6f;
    Vector3 boundDirection;
    private void OnEnable()
    {
        boundDirection = Vector3.up;
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
            
            if(UIManager.instance.imageBuffPanel.gameObject.activeSelf == true)
            {
                UIManager.instance.OffPanel();
            }
            UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
            BuffStatus.instance.RandomBuffBody();
            BuffStatus.instance.RandomBuffIcon();
            // UIManager.instance.SetBuffBody("Magazine"); // Magazine or Gun
            // UIManager.instance.SetBuffIcon("Large");    // 
            GameManager.instance.isSpawnItem = false;
            ObjectPoolManager.ReturnItem(this);
            
        }
    }
}
