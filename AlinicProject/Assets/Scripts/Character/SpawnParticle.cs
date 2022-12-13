using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * ź�� ���� ��ũ��Ʈ
 * 
 */
public class SpawnParticle : MonoBehaviour
{
    [HideInInspector] public GameObject firePoint;  // �ѱ�
    //public RotateToMouse rotateToMouse; 
    private Vector3[] shotgunSpread;                // ������ ź���� ��ġ
    private float spread = 3f;                      // ������ ź���� �Ÿ�
    public string currentBullet;
    void Start()
    {
        currentBullet = "Normal";
        shotgunSpread = new Vector3[4];
        shotgunSpread[0] = new Vector3(0.0f, 0.0f, 0.0f); // �߾�
        shotgunSpread[1] = new Vector3(0.0f, spread, 0.0f); // 12�� ����
        shotgunSpread[2] = new Vector3(0.0f, -spread, spread); // 5�� ����
        shotgunSpread[3] = new Vector3(0.0f, -spread, -spread); // 7�� ����
    }
    // ����Ʈ ȣ��
    public void SetEffect()
    {
        MoveParticle effects;

        if (firePoint != null)
        {
            //effects = ObjectPoolManager.GetBullet(firePoint.transform.position);
            //Effects.transform.localRotation = firePoint.transform.rotation;
            if (!GameManager.instance.playableWeapon.name.Replace("(Clone)", "").Equals("Shotgun"))
            {
                effects = ObjectPoolManager.GetBullet(firePoint.transform.position, currentBullet);
            }
            else
            {
                foreach (var _spread in shotgunSpread) {
                    effects = ObjectPoolManager.GetBullet(firePoint.transform.position, _spread, currentBullet);
                }
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
    public void setReturnBullet(MoveParticle obj)
    {
        ObjectPoolManager.ReturnBullet(obj, currentBullet);
    }

}
