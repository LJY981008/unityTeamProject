using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField]
    private GameObject poolingBullet;
    Queue<Bullet> poolingBulletQueue = new Queue<Bullet>();


    private void Awake()
    {
        if (instance == null) instance = this;
        
    }
    private void Start()
    {
        BulletInitialize(10);
    }
    // 큐 활성화
    private void BulletInitialize(int index)
    {
        for(int i = 0; i < index; i++)
        {
            poolingBulletQueue.Enqueue(CreateNewBullet());
        }
    }
    private Bullet CreateNewBullet()
    {
        var obj = Instantiate<GameObject>(Ex_ResourcesManager.ammo).GetComponent<Bullet>();
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }
    public static Bullet GetBullet(Transform gun, Vector3 spawnPos)
    {
        if(instance.poolingBulletQueue.Count > 0)
        {
            var bullet = instance.poolingBulletQueue.Dequeue();
            bullet.transform.position = spawnPos;
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateNewBullet();
            newBullet.transform.position = spawnPos;
            newBullet.gameObject.SetActive(true);
            newBullet.transform.SetParent(null);
            return newBullet;
        }
    }
    public static void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(instance.transform);
        instance.poolingBulletQueue.Enqueue(bullet);
    }
}
