using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public static ApplyBuff instance;
    public GameObject objMonster;
    public bool isSlow;
    public bool isBurn;
    public bool isForce;

    public bool isDamage;

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
    }
    private void Start()
    {
        slowColor = new Color(0.06f, 0.6f, 1.0f);
        burnColor = new Color(1, 0, 0);
        forceColor = new Color(0.2f, 0.2f, 0.2f);
        forceEmissionColor = Color.black;
        currentMonsterModel = FindMaterial(objMonster.transform, "1_Model");
        currentMonsterSkin = FindMaterial(currentMonsterModel, "ironreaver01").GetComponent<SkinnedMeshRenderer>();
        SetDefaultColor();
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
    private IEnumerator MonsterSlow()
    {
        InitMonster.Instance.actionSpeedDown();
        while (isDamage)
        {
            currentMonsterSkin.materials[0].color = slowColor;
            isDamage = false;
            yield return new WaitForSecondsRealtime(10f);
        }
        isSlow = false;
        currentMonsterSkin.materials[0].color = originColor;
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
        currentMonsterSkin.materials[0].color = originColor;
        burnElapsed = 0;
        burnStack = 0;
    }
    private IEnumerator MonsterForce()
    {
        InitMonster.Instance.actionStun();
        yield return new WaitForSeconds(3f);
        InitMonster.Instance.afterStun();
        currentMonsterSkin.materials[0].color = originColor;
        currentMonsterSkin.materials[0].SetColor("_EmissionColor", originEmissionColor);
        yield return new WaitForSeconds(7f);
        forceCoolTime = false;

    }
    public void SetBurnHitCount()
    {
        hitBurnCount++;
        if (hitBurnCount == 3)
        {
            burnElapsed = 0;
            if (currentMonsterSkin.materials[0].color != burnColor)
                currentMonsterSkin.materials[0].color = burnColor;
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
            Debug.Log("³»ºÎÄð");
        if(hitForceCount == 10)
        {
            forceCoolTime = true;
            if (currentMonsterSkin.materials[0].color != forceColor)
            {
                currentMonsterSkin.materials[0].color = forceColor;
                currentMonsterSkin.materials[0].SetColor("_EmissionColor", forceEmissionColor);
            }
        }
        else if (hitForceCount > 10)
        {
            hitForceCount = 1;
        }
    }
    public void DoCoroutine(string name)
    {
        StartCoroutine(name);
    }
    private void SetDefaultColor()
    {
        originColor = currentMonsterSkin.materials[0].color;
        originEmissionColor = currentMonsterSkin.materials[0].GetColor("_EmissionColor");
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
}
