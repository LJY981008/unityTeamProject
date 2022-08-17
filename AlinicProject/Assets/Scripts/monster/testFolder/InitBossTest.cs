using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InitBossTest : MonoBehaviour
{
    Transform childT;
    GameObject player;
    Animator ani;
    // Start is called before the first frame update
    private void Awake()
    {
        childT = transform.Find("1_Model");
        player = GameObject.Find("Player");
        ani = childT.GetComponent<Animator>();
    }
    void Start()
    {
        ani.SetInteger("aniInt", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (getDistanceToTarget() <= 120f)
        {
            childT.gameObject.SetActive(true);
        }
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}
