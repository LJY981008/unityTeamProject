using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField]
    private GameObject poolingBullet;
    Queue<MoveParticle> poolingBulletQueue = new Queue<MoveParticle>();
    private GameObject poolingGun;
    Queue<PlayerUpper> poolingGunQueue = new Queue<PlayerUpper>();

    private GameObject ammo;

    private void Awake()
    {
        if (instance == null) instance = this;
        
    }
    private void Start()
    {
        BulletInitialize(10);
    }

    private void GunInitialize()
    {
        List<GameObject> listGuns = Ex_ResourcesManager.instance.playableWeapons;
        foreach (GameObject obj in listGuns)
        {
            poolingGunQueue.Enqueue(CreateNewGun(obj));
        }
    }
    private PlayerUpper CreateNewGun(GameObject obj)
    {
        var newObj = Instantiate<GameObject>(obj).AddComponent<PlayerUpper>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    // 조건문으로 원하는 총 나올때까지
    /*private static PlayerUpper GetGun(string gunName)
    {
        //var instance.poolingGunQueue.Dequeue();
    }*/

    // 총알 풀링
    private void BulletInitialize(int index)
    {
        for(int i = 0; i < index; i++)
        {
            poolingBulletQueue.Enqueue(CreateNewBullet());
        }
    }
    private MoveParticle CreateNewBullet()
    {
        ammo = Ex_ResourcesManager.ammo;
        var newObj = Instantiate<GameObject>(ammo).GetComponent<MoveParticle>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public static MoveParticle GetBullet(Vector3 spawnPos)
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
    public static void ReturnBullet(MoveParticle bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(instance.transform);
        instance.poolingBulletQueue.Enqueue(bullet);
    }
}
