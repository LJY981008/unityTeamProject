using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 오브젝트 풀링 매니저
 */
public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;

    private Queue<MoveParticle> QueuepoolingBullet = new Queue<MoveParticle>(); // 총알의 목록
    private List<PlayerUpper> listPoolingGun = new List<PlayerUpper>();         // 무기의 목록
    private PlayerBody playerBox;                                               // 플레이어 본체
    private GameObject ammo;                                                    // 총알

    private void Awake()
    {
        instance = this;
        playerBox = GameManager.instance.playerBody;
        GunInitialize();
        BulletInitialize(10);
    }
    // 무기 오브젝트 호출 함수
    private void GunInitialize()
    {
        List<GameObject> listGuns = ResourcesManager.instance.playableWeapons;
        foreach (GameObject obj in listGuns)
        {
            instance.listPoolingGun.Add(instance.CreateNewGun(obj));
        }
    }
    // 무기 오브젝트 생성 및 비활성화 함수
    public PlayerUpper CreateNewGun(GameObject obj)
    {
        var newObj = Instantiate<GameObject>(obj, Vector3.zero, Quaternion.identity).GetComponent<PlayerUpper>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(instance.playerBox.transform);
        return newObj;
    }
    // 선택한 무기 오브젝트 활성화 함수
    public static PlayerUpper GetGun(string weaponName)
    {
        Vector3 spawnPos = new Vector3(0.0f, 1.6f, 0.0f);
        var obj = instance.listPoolingGun.Find(o => o.name.Contains(weaponName));
        obj.transform.localPosition = spawnPos;
        obj.gameObject.SetActive(true);
        return obj;
    }
    // 무기 반환 함수
    public static void ReturnGun(PlayerUpper player)
    {
        player.gameObject.SetActive(false);
    }

    // 총알 오브젝트 호출 함수
    private void BulletInitialize(int index)
    {
        for(int i = 0; i < index; i++)
        {
            instance.QueuepoolingBullet.Enqueue(instance.CreateNewBullet());
        }
    }
    // 총알 생성 및 비활성화 함수
    private MoveParticle CreateNewBullet()
    {
        ammo = ResourcesManager.instance.ammo;
        var newObj = Instantiate<GameObject>(ammo).GetComponent<MoveParticle>();
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(null);
        return newObj;
    }
    // 샷건의 총알 오브젝트 활성화 함수
    public static MoveParticle GetBullet(Vector3 spawnPos, Vector3 shotgunPos)
    {
        if(instance.QueuepoolingBullet.Count > 0)
        {
            var bullet = instance.QueuepoolingBullet.Dequeue();
            bullet.transform.position = spawnPos;
            bullet.shotgunShotPos = shotgunPos;
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateNewBullet();
            newBullet.transform.position = spawnPos;
            newBullet.shotgunShotPos = shotgunPos;
            newBullet.gameObject.SetActive(true);
            newBullet.transform.SetParent(null);
            return newBullet;
        }
    }
    // 샷건을 제외한 무기의 총알 반환 함수
    public static MoveParticle GetBullet(Vector3 spawnPos)
    {
        if (instance.QueuepoolingBullet.Count > 0)
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
    // 총알 반환 함수
    public static void ReturnBullet(MoveParticle bullet)
    {
        bullet.gameObject.SetActive(false);
        instance.QueuepoolingBullet.Enqueue(bullet);
    }
}
