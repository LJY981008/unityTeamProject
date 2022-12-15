using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 오브젝트 풀링 매니저
 */
public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;
    private Dictionary<string, Queue<MoveParticle>> dicBullet = new Dictionary<string, Queue<MoveParticle>>();
    private Queue<MoveParticle> QueuePoolingBulletNormal = new Queue<MoveParticle>(); // 총알의 목록
    private Queue<MoveParticle> QueuePoolingBulletIce = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletGrenade = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletFire = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletCorrosion = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletForce = new Queue<MoveParticle>();
    private List<PlayerUpper> listPoolingGun = new List<PlayerUpper>();         // 무기의 목록
    private PlayerBody playerBox;                                               // 플레이어 본체
    private GameObject ammo;                                                    // 총알
    private GameObject buffItemObject;
    private BuffItem buffItemScript;
    private void Awake()
    {
        instance = this;
        playerBox = GameManager.instance.playerBody;
        GunInitialize();
        BulletInitialize(10);
        ItemInitialize();
    }
    // 버프 아이템 오브젝트 호출 함수
    private void ItemInitialize()
    {
        buffItemObject = ResourcesManager.instance.buffItem;
        buffItemScript = CreateNewItem(buffItemObject);
    }
    // 버프 아이템 생성 함수
    public BuffItem CreateNewItem(GameObject obj)
    {
        var newObj = Instantiate<GameObject>(obj, Vector3.zero, Quaternion.identity).GetComponent<BuffItem>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(instance.playerBox.transform);
        return newObj;
    }
    // 버프 아이템 가져오는 함수
    public static BuffItem GetItem()
    {
        Vector3 spawnPos = PlayerUtill.GetRandomMapPos(instance.playerBox.transform.position);
        var obj = instance.buffItemScript;
        obj.transform.SetParent(null);
        obj.transform.localPosition = spawnPos;
        obj.gameObject.SetActive(true);
        return obj;
    }
    // 버프 아이템 반환 함수
    public static void ReturnItem(BuffItem item)
    {
        item.transform.SetParent(instance.playerBox.transform);
        item.gameObject.SetActive(false);
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
        ApplyBuff.instance.weapons.Add(newObj.transform.name.Replace("(Clone)", ""), newObj);
        UIManager.instance.skillCoolTime.Add(newObj.transform.name.Replace("(Clone)", ""), newObj.gunData.skillCoolTime);
        GameManager.instance.currentSkillCoolTime.Add(newObj.transform.name.Replace("(Clone)", ""), newObj.gunData.currentCoolTime);
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
        obj.gunData.currentCoolTime = GameManager.instance.currentSkillCoolTime[weaponName.Replace("(Clone)", "")];
        obj.gameObject.SetActive(true);
        return obj;
    }
    // 무기 반환 함수
    public static void ReturnGun(PlayerUpper player)
    {
        player.gameObject.SetActive(false);
    }
    /**
     * Shoot_01 노랑 기본
     * Shoot_02 파랑 빙결
     * Shoot_04 폭발 유탄
     * Shoot_05 빨강 화상
     * Shoot_08 보라 부식
     * Shoot_13 분홍 무력
     */
    // 총알 오브젝트 호출 함수
    private void BulletInitialize(int index)
    {
        for(int i = 0; i < index; i++)
        {
            instance.QueuePoolingBulletNormal.Enqueue(instance.CreateNewBullet("Shoot_01"));
            instance.QueuePoolingBulletIce.Enqueue(instance.CreateNewBullet("Shoot_02"));
            instance.QueuePoolingBulletGrenade.Enqueue(instance.CreateNewBullet("Shoot_04"));
            instance.QueuePoolingBulletFire.Enqueue(instance.CreateNewBullet("Shoot_05"));
            instance.QueuePoolingBulletCorrosion.Enqueue(instance.CreateNewBullet("Shoot_08"));
            instance.QueuePoolingBulletForce.Enqueue(instance.CreateNewBullet("Shoot_13"));
        }
        dicBullet.Add("Normal", QueuePoolingBulletNormal);
        dicBullet.Add("Ice", QueuePoolingBulletIce);
        dicBullet.Add("Grenade", QueuePoolingBulletGrenade);
        dicBullet.Add("Fire", QueuePoolingBulletFire);
        dicBullet.Add("Corrosion", QueuePoolingBulletCorrosion);
        dicBullet.Add("Force", QueuePoolingBulletForce);
    }
    // 총알 생성 및 비활성화 함수
    private MoveParticle CreateNewBullet(string bulletName)
    {
        ammo = ResourcesManager.instance.ammo.Find(o => o.name.Contains(bulletName));
        var newObj = Instantiate<GameObject>(ammo).GetComponent<MoveParticle>();
        newObj.spawnParticle = gameObject.GetComponent<SpawnParticle>();
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(null);
        return newObj;
    }
    // 샷건의 총알 오브젝트 활성화 함수
    public static MoveParticle GetBullet(Vector3 spawnPos, Vector3 shotgunPos, string bulletName)
    {
        if(instance.dicBullet[bulletName].Count > 0)
        {
            //var bullet = instance.QueuePoolingBulletNormal.Dequeue();
            var bullet = instance.dicBullet[bulletName].Dequeue();
            bullet.transform.position = spawnPos;
            bullet.shotgunShotPos = shotgunPos;
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateNewBullet(bulletName);
            newBullet.transform.position = spawnPos;
            newBullet.shotgunShotPos = shotgunPos;
            newBullet.gameObject.SetActive(true);
            newBullet.transform.SetParent(null);
            return newBullet;
        }
    }
    // 샷건을 제외한 무기의 총알 반환 함수
    public static MoveParticle GetBullet(Vector3 spawnPos, string bulletName)
    {
        if (instance.dicBullet[bulletName].Count > 0)
        {
            var bullet = instance.dicBullet[bulletName].Dequeue();
            bullet.transform.position = spawnPos;
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateNewBullet(bulletName);
            newBullet.transform.position = spawnPos;
            newBullet.gameObject.SetActive(true);
            newBullet.transform.SetParent(null);
            return newBullet;
        }
    }
    // 총알 반환 함수
    public static void ReturnBullet(MoveParticle bullet, string bulletName)
    {
        bullet.gameObject.SetActive(false);
        instance.dicBullet[bulletName].Enqueue(bullet);
    }
}
