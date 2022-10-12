using System.Collections;
using System.Reflection;
using UnityEngine;

public class RazerEffectTestScript : MonoBehaviour
{
    public static RazerEffectTestScript instance;
    public GameObject effect;
    public GameObject firePoint;
    private GameObject effectToSpawn;
    public Animator mobAni;
    public GameObject player;
    public RotateToPlayer rotateToPlayer;
    Vector3 playerPos;
    float a;

    [SerializeField] GameObject missile;
    [SerializeField] GameObject MuzzleEffect;
    [SerializeField] Transform missilePoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        a = 0.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = effect;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = BossScript.instance.target.transform.position;
    }

    public void testEffect()
    {
        SpawnEffects();
    }

    void SpawnEffects()
    {
        GameObject Effects;

        if (firePoint != null)
        {
            Effects = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            Effects.transform.localRotation = rotateToPlayer.GetRotation();
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }

    public void startC()
    {
        StartCoroutine(testCoroutine());
    }

    IEnumerator testCoroutine() 
    {
        yield return new WaitForSeconds(0.5f);
        testEffect();
    }

    public void StartMissileCor()
    {
        StartCoroutine(MissileCor());
    }

    IEnumerator MissileCor()
    {
        yield return new WaitForSeconds(3.0f);
        ShootMissile();
    }

    public void ShootMissile()
    {
        var muzzlePos = Instantiate(MuzzleEffect, missilePoint.transform.position, Quaternion.identity);
        muzzlePos.transform.forward = missilePoint.gameObject.transform.forward;
        var targetMissile = Instantiate(missile, missilePoint.transform.position, Quaternion.identity);
        targetMissile.transform.localRotation = rotateToPlayer.GetRotation();
    }
}
