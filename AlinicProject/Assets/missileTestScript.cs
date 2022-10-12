using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileTestScript : MonoBehaviour
{
    [SerializeField] GameObject missile;
    [SerializeField] GameObject MuzzleEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootMissile()
    {
        var muzzlePos = Instantiate(MuzzleEffect, transform.position, Quaternion.identity);
        muzzlePos.transform.forward = gameObject.transform.forward;
        var muzzlePosPart = muzzlePos.GetComponent<ParticleSystem>();
        if (muzzlePos != null)
        {
            Destroy(muzzlePos, muzzlePosPart.main.duration);
        }
        var targetMissile = Instantiate(missile, transform.position, Quaternion.identity);
        targetMissile.transform.localRotation = RazerEffectTestScript.instance.rotateToPlayer.GetRotation();
    }
}
