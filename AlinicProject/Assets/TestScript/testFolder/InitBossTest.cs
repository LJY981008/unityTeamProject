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
        // 타겟과의 거리가 120 이상일때
        if (getDistanceToTarget() <= 120f)
        {
            // 1_Model 활성화
            childT.gameObject.SetActive(true);
        }
    }

    public float getDistanceToTarget()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    
}
