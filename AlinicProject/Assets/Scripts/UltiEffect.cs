using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiEffect : MonoBehaviour
{
    public GameObject effectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("누르는중");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnVFX();
        }
    }
    public void SpawnVFX()
    {
        var vfx = Instantiate(effectToSpawn);

        var ps = GetFirstPS(vfx);

        Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax + 1);
    }
    public ParticleSystem GetFirstPS(GameObject vfx)
    {
        var ps = vfx.GetComponent<ParticleSystem>();
        if (ps == null && vfx.transform.childCount > 0)
        {
            foreach (Transform t in vfx.transform)
            {
                ps = t.GetComponent<ParticleSystem>();
                if (ps != null)
                    return ps;
            }
        }
        return ps;
    }
}
