using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayerTest : MonoBehaviour
{
    //플레이어 이동속도
    private float speedMove;
    public float _speedMove
    {
        get { return speedMove; }
        set { speedMove = value; }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        _speedMove = 5.5f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    //wasd 이동함수
    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speedMove;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speedMove;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speedMove;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speedMove;
        }
    }
}
