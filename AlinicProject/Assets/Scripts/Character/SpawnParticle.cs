using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject[] Effects;
    //public RotateToMouse rotateToMouse;
    private GameObject effectToSpawn;

    void Start()
    {
        effectToSpawn = Effects[0];
    }

    public void SetEffect()
    {
        GameObject Effects;

        if (firePoint != null)
        {
            ObjectPoolManager.GetBullet(firePoint.transform.position);
            transform.localRotation = firePoint.transform.rotation;
           /* Effects = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            Effects.transform.localRotation = firePoint.transform.rotation;*/
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
