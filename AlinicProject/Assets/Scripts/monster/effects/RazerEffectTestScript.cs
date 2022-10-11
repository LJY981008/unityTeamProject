using System.Collections;
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
        if (Input.GetMouseButton(0))
        {
            mobAni.SetInteger("aniInt", 1);
        }
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
}
