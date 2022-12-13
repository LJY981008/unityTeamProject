using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * ≈∫æÀ Ω∫∆˘ Ω∫≈©∏≥∆Æ
 * 
 */
public class SpawnParticle : MonoBehaviour
{
    [HideInInspector] public GameObject firePoint;  // √—±∏
    //public RotateToMouse rotateToMouse; 
    private Vector3[] shotgunSpread;                // º¶∞«¿« ≈∫∆€¡¸ ¿ßƒ°
    private float spread = 3f;                      // º¶∞«¿« ≈∫∆€¡¸ ∞≈∏Æ
    public string currentBullet;
    void Start()
    {
        currentBullet = "Normal";
        shotgunSpread = new Vector3[4];
        shotgunSpread[0] = new Vector3(0.0f, 0.0f, 0.0f); // ¡ﬂæ”
        shotgunSpread[1] = new Vector3(0.0f, spread, 0.0f); // 12Ω√ πÊ«‚
        shotgunSpread[2] = new Vector3(0.0f, -spread, spread); // 5Ω√ πÊ«‚
        shotgunSpread[3] = new Vector3(0.0f, -spread, -spread); // 7Ω√ πÊ«‚
    }
    // ¿Ã∆Â∆Æ »£√‚
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
