using System.Collections;
using System.Reflection;
using Unity.VisualScripting;
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

    [SerializeField] GameObject missile;
    [SerializeField] GameObject MuzzleEffect;
    [SerializeField] RotateToPlayer rotateToPlayerforMissileLeft;
    [SerializeField] RotateToPlayer rotateToPlayerforMissileRight;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = effect;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = InitMonster.Instance.target.transform.position;
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
        yield return new WaitForSeconds(2.7f);
        ShootMissile(rotateToPlayerforMissileLeft.transform.position, rotateToPlayerforMissileRight);
        ShootMissile(rotateToPlayerforMissileRight.transform.position, rotateToPlayerforMissileLeft);
    }


    public void ShootMissile(Vector3 pos, RotateToPlayer missileMuzzle)
    {
        var muzzlePos = Instantiate(MuzzleEffect, pos, Quaternion.identity);
        muzzlePos.transform.forward = missileMuzzle.gameObject.transform.forward;
        var targetMissile = Instantiate(missile, pos, Quaternion.identity);
        targetMissile.transform.localRotation = missileMuzzle.GetRotation();
    }
}
