using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * ������Ʈ Ǯ�� �Ŵ���
 */
public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;
    private Dictionary<string, Queue<MoveParticle>> dicBullet = new Dictionary<string, Queue<MoveParticle>>();
    private Queue<MoveParticle> QueuePoolingBulletNormal = new Queue<MoveParticle>(); // �Ѿ��� ���
    private Queue<MoveParticle> QueuePoolingBulletIce = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletGrenade = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletFire = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletCorrosion = new Queue<MoveParticle>();
    private Queue<MoveParticle> QueuePoolingBulletForce = new Queue<MoveParticle>();
    private List<PlayerUpper> listPoolingGun = new List<PlayerUpper>();         // ������ ���
    private PlayerBody playerBox;                                               // �÷��̾� ��ü
    private GameObject ammo;                                                    // �Ѿ�
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
    // ���� ������ ������Ʈ ȣ�� �Լ�
    private void ItemInitialize()
    {
        buffItemObject = ResourcesManager.instance.buffItem;
        buffItemScript = CreateNewItem(buffItemObject);
    }
    // ���� ������ ���� �Լ�
    public BuffItem CreateNewItem(GameObject obj)
    {
        var newObj = Instantiate<GameObject>(obj, Vector3.zero, Quaternion.identity).GetComponent<BuffItem>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(instance.playerBox.transform);
        return newObj;
    }
    // ���� ������ �������� �Լ�
    public static BuffItem GetItem()
    {
        Vector3 spawnPos = PlayerUtill.GetRandomMapPos(instance.playerBox.transform.position);
        var obj = instance.buffItemScript;
        obj.transform.SetParent(null);
        obj.transform.localPosition = spawnPos;
        obj.gameObject.SetActive(true);
        return obj;
    }
    // ���� ������ ��ȯ �Լ�
    public static void ReturnItem(BuffItem item)
    {
        item.transform.SetParent(instance.playerBox.transform);
        item.gameObject.SetActive(false);
    }

    // ���� ������Ʈ ȣ�� �Լ�
    private void GunInitialize()
    {
        List<GameObject> listGuns = ResourcesManager.instance.playableWeapons;
        foreach (GameObject obj in listGuns)
        {
            instance.listPoolingGun.Add(instance.CreateNewGun(obj));
        }
    }
    // ���� ������Ʈ ���� �� ��Ȱ��ȭ �Լ�
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
    // ������ ���� ������Ʈ Ȱ��ȭ �Լ�
    public static PlayerUpper GetGun(string weaponName)
    {
        Vector3 spawnPos = new Vector3(0.0f, 1.6f, 0.0f);
        var obj = instance.listPoolingGun.Find(o => o.name.Contains(weaponName));
        obj.transform.localPosition = spawnPos;
        obj.gunData.currentCoolTime = GameManager.instance.currentSkillCoolTime[weaponName.Replace("(Clone)", "")];
        obj.gameObject.SetActive(true);
        return obj;
    }
    // ���� ��ȯ �Լ�
    public static void ReturnGun(PlayerUpper player)
    {
        player.gameObject.SetActive(false);
    }
    /**
     * Shoot_01 ��� �⺻
     * Shoot_02 �Ķ� ����
     * Shoot_04 ���� ��ź
     * Shoot_05 ���� ȭ��
     * Shoot_08 ���� �ν�
     * Shoot_13 ��ȫ ����
     */
    // �Ѿ� ������Ʈ ȣ�� �Լ�
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
    // �Ѿ� ���� �� ��Ȱ��ȭ �Լ�
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
    // ������ �Ѿ� ������Ʈ Ȱ��ȭ �Լ�
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
    // ������ ������ ������ �Ѿ� ��ȯ �Լ�
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
    // �Ѿ� ��ȯ �Լ�
    public static void ReturnBullet(MoveParticle bullet, string bulletName)
    {
        bullet.gameObject.SetActive(false);
        instance.dicBullet[bulletName].Enqueue(bullet);
    }
}
