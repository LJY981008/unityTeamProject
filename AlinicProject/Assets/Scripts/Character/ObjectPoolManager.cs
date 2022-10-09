using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [HideInInspector]
    public static ObjectPoolManager instance;
    [SerializeField]
    private GameObject poolingBullet;
    Queue<MoveParticle> QueuepoolingBullet = new Queue<MoveParticle>();
    private GameObject poolingGun;
    List<PlayerUpper> listPoolingGun = new List<PlayerUpper>();
    private GameObject playerBox;
    private GameObject ammo;

    private void Awake()
    {
        instance = this;
        playerBox = Ex_GameManager.instance.playerBox;
        GunInitialize();
        BulletInitialize(10);
    }
    // gun 오브젝트 스폰
    private void GunInitialize()
    {
        List<GameObject> listGuns = Ex_ResourcesManager.instance.playableWeapons;
        foreach (GameObject obj in listGuns)
        {
            instance.listPoolingGun.Add(instance.CreateNewGun(obj));
        }
    }
    public PlayerUpper CreateNewGun(GameObject obj)
    {
        var newObj = Instantiate<GameObject>(obj, Vector3.zero, Quaternion.identity).GetComponent<PlayerUpper>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(instance.playerBox.transform);
        return newObj;
    }

    public static PlayerUpper GetGun(string weaponName, Vector3 spawnPos)
    {

        var obj = instance.listPoolingGun.Find(o => o.name.Contains(weaponName));
        obj.transform.position = spawnPos;
        obj.gameObject.SetActive(true);
        return obj;
    }
    public static void ReturnGun(PlayerUpper player)
    {
        player.gameObject.SetActive(false);
    }

    // 총알 풀링
    private void BulletInitialize(int index)
    {
        for(int i = 0; i < index; i++)
        {
            instance.QueuepoolingBullet.Enqueue(instance.CreateNewBullet());
        }
    }
    private MoveParticle CreateNewBullet()
    {
        ammo = Ex_ResourcesManager.ammo;
        var newObj = Instantiate<GameObject>(ammo).GetComponent<MoveParticle>();
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(null);
        return newObj;
    }
    public static MoveParticle GetBullet(Vector3 spawnPos)
    {
        if(instance.QueuepoolingBullet.Count > 0)
        {
            var bullet = instance.QueuepoolingBullet.Dequeue();
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
        instance.QueuepoolingBullet.Enqueue(bullet);
    }
}
