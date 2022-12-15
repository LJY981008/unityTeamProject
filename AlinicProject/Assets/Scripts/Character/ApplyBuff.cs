using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public static ApplyBuff instance;
    public GameObject objMonster;
    public Dictionary<string, PlayerUpper> weapons = new Dictionary<string, PlayerUpper>();
    public Dictionary<string, int> buffLargeMagazineSize = new Dictionary<string, int>();
    public bool isSlow;
    public bool isBurn;
    public bool isForce;
    public bool isDamage;

    private Material monsterMaterial;
    private Color originColor;
    private Color originEmissionColor;
    private Color slowColor;
    private Color burnColor;
    private Color forceColor;
    private Color forceEmissionColor;
    private int hitBurnCount;
    private int burnStack;
    private int burnDamage;
    private int burnDuration;
    private int burnElapsed;
    private int hitForceCount;
    private int forceDuration;
    private int forceElapsed;
    private bool forceCoolTime;
    private bool isChange;
    private float buffLightMagazineDuration;
    private float buffLargeMagazineDuration;
    private float buffIceMagazineDuration;
    private float buffIceMagazineState;
    private float buffLightMagazineSpeed;
    private Transform currentMonsterModel;
 
    private SkinnedMeshRenderer currentMonsterSkin;
    private SpawnParticle spawnParticle;

    public int HitBurnCount
    {
        set { hitBurnCount = value; }
        get { return hitBurnCount; }
    }
    public int HitForceCount
    {
        set { hitForceCount = value; }
        get { return hitForceCount; }
    }
    private void Awake()
    {
        instance = this;
        isSlow = false;
        isBurn = false;
        isForce = false;
        isChange = false;
    }
    private void Start()
    {
        slowColor = new Color(0.06f, 0.6f, 1.0f);
        burnColor = new Color(1, 0, 0);
        forceColor = new Color(0.2f, 0.2f, 0.2f);
        buffLightMagazineDuration = 60f;
        buffLargeMagazineDuration = 40f;
        buffIceMagazineDuration = 30f;
        buffLightMagazineSpeed = 1.2f;
        buffLargeMagazineSize.Add("Pistol", 10);
        buffLargeMagazineSize.Add("Rifle", 15);
        buffLargeMagazineSize.Add("Shotgun", 5);
        buffIceMagazineState = 0.2f;
        forceEmissionColor = Color.black;
        currentMonsterModel = FindMaterial(objMonster.transform, "1_Model");
        monsterMaterial = FindMaterial(currentMonsterModel, "ironreaver01").GetComponent<SkinnedMeshRenderer>().materials[0];
        originColor = monsterMaterial.color;
        originEmissionColor = monsterMaterial.GetColor("_EmissionColor");
        SetDefault();
        
    }
    private IEnumerator MonsterSlow()
    {
        InitMonster.Instance.actionSpeedDown();
        while (isDamage)
        {
            monsterMaterial.color = slowColor;
            isDamage = false;
            yield return new WaitForSecondsRealtime(10f);
        }
        isSlow = false;
        monsterMaterial.color = originColor;
        InitMonster.Instance.afterSpeedDown();
    }
    private IEnumerator MonsterBurn()
    {
        while (burnElapsed < burnDuration)
        {
            InitMonster.Instance.onDamage(burnDamage * burnStack);
            burnElapsed++;
            yield return new WaitForSeconds(1f);
        }
        isBurn = false;
        monsterMaterial.color = originColor;
        burnElapsed = 0;
        burnStack = 0;
    }
    private IEnumerator MonsterForce()
    {
        InitMonster.Instance.actionStun();
        yield return new WaitForSeconds(3f);
        InitMonster.Instance.afterStun();
        monsterMaterial.color = originColor;
        monsterMaterial.SetColor("_EmissionColor", originEmissionColor);
        yield return new WaitForSeconds(7f);
        forceCoolTime = false;
    }
    private IEnumerator BuffLightMagazine()
    {
        float prevBuffSpeed = GameManager.instance.buffSpeed;
        GameManager.instance.buffSpeed = buffLightMagazineSpeed;
        yield return new WaitForSecondsRealtime(buffLightMagazineDuration);
        GameManager.instance.buffSpeed = prevBuffSpeed;
    }
    private IEnumerator BuffLargeMagazine()
    {
        Debug.Log("코");
        foreach(var weapon in weapons)
        {
            weapons[weapon.Key].gunData.maxAmmo += buffLargeMagazineSize[weapon.Key];
            weapons[weapon.Key].gunData.currentAmmo += buffLargeMagazineSize[weapon.Key];
        }
        UIManager.instance.textCurrentAmmo.text = GameManager.instance.playableWeapon.gunData.currentAmmo.ToString();
        UIManager.instance.textMaxAmmo.text = GameManager.instance.playableWeapon.gunData.maxAmmo.ToString();
        Debug.Log("루");
        yield return new WaitForSecondsRealtime(buffLargeMagazineDuration);
        Debug.Log("틴");
        foreach(var weapon in weapons)
        {
            weapons[weapon.Key].gunData.maxAmmo -= buffLargeMagazineSize[weapon.Key];
            if (weapons[weapon.Key].gunData.currentAmmo > weapons[weapon.Key].gunData.maxAmmo)
            {
                weapons[weapon.Key].gunData.currentAmmo = weapons[weapon.Key].gunData.maxAmmo;
            }
        }
        UIManager.instance.textCurrentAmmo.text = GameManager.instance.playableWeapon.gunData.currentAmmo.ToString();
        UIManager.instance.textMaxAmmo.text = GameManager.instance.playableWeapon.gunData.maxAmmo.ToString();
    }
    /*private IEnumerator BuffIceMagazine()
    {
        int prev = UIManager.instance.skillCoolTime;
        UIManager.instance.skillCoolTime -= (int)(prev * buffIceMagazineState);
        yield return new WaitForSecondsRealtime(buffIceMagazineDuration);
        UIManager.instance.skillCoolTime = prev;
    }*/
    public void SetBurnHitCount()
    {
        hitBurnCount++;
        if (hitBurnCount == 3)
        {
            burnElapsed = 0;
            if (monsterMaterial.color != burnColor)
                monsterMaterial.color = burnColor;
            if (burnStack < 5)
                burnStack++;
        }
        else if (hitBurnCount > 3)
        {
            hitBurnCount = 1;
        }
    }
    public void SetForceHitCount()
    {
        if (!forceCoolTime)
            hitForceCount++;
        else
            Debug.Log("내부쿨");
        if(hitForceCount == 10)
        {
            forceCoolTime = true;
            if (monsterMaterial.color != forceColor)
            {
                monsterMaterial.color = forceColor;
                monsterMaterial.SetColor("_EmissionColor", forceEmissionColor);
            }
        }
        else if (hitForceCount > 10)
        {
            hitForceCount = 1;
        }
    }
    public void DoCoroutine(string name)
    {
        Debug.Log("D : " + name);
        StartCoroutine(name);
        Debug.Log("o");
    }
    private void SetDefault()
    {
        
        hitBurnCount = 0;
        burnStack = 0;
        burnDamage = 5;
        burnDuration = 5;
        burnElapsed = 0;

        hitForceCount = 0;
        forceDuration = 3;
        forceElapsed = 0;
        forceCoolTime = false;
    }
    
    private Transform FindMaterial(Transform tr, string name)
    {
        if(tr.name.Equals(name))
            return tr;
        for (int i = 0; i < tr.childCount; i++)
        {
            Transform childTr = FindMaterial(tr.GetChild(i), name);
            if (childTr != null) return childTr;
        }
        return null;
    }
    private void OnDestroy()
    {
        foreach (var weapon in weapons)
        {
            weapons[weapon.Key].gunData.maxAmmo -= buffLargeMagazineSize[weapon.Key];
            if (weapons[weapon.Key].gunData.currentAmmo > weapons[weapon.Key].gunData.maxAmmo)
            {
                weapons[weapon.Key].gunData.currentAmmo = weapons[weapon.Key].gunData.maxAmmo;
            }
        }
    }
}
