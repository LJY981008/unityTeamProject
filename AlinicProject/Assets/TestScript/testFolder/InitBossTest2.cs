using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InitBossTest2 : MonoBehaviour
{
    Transform _childT;
    Animator ani;
    public int triggerInt;

    public Transform childT
    {
        get { return _childT; }
        set { _childT = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _childT = transform.Find("1_Model");
        ani = _childT.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerInt == 2)
        {
            _childT.gameObject.SetActive(false);
        }
    }

    public void doCoroutine()
    {
        StartCoroutine("test");
    }
    IEnumerable test()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        triggerInt = 2;
    }

    public void doInvoke()
    {
        Invoke("test2", 6.0f);
    }

    void test2()
    {
        triggerInt = 2;
    }
}
