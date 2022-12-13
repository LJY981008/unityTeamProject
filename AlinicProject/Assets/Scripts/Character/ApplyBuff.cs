using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public static ApplyBuff instance;
    public GameObject objMonster;
    public bool isSlow;
    public bool isBurn;

    public bool isDamage;

    private Color originColor;
    private Color slowColor;
    private Color burnColor;
    private int hitBurnCount;
    private int burnStack;
    private int burnDamage;
    private int burnDuration;
    private int burnElapsed;

    private Transform currentMonsterModel;
    private SkinnedMeshRenderer currentMonsterSkin;
    private SpawnParticle spawnParticle;

    public int HitBurnCount
    {
        set { hitBurnCount = value; }
        get { return hitBurnCount; }
    }
    private void Awake()
    {
        instance = this;
        isSlow = false;
        isBurn = false;
    }
    private void Start()
    {
        slowColor = new Color(0.06f, 0.6f, 1.0f);
        burnColor = new Color(1, 0, 0);
        currentMonsterModel = FindMaterial(objMonster.transform, "1_Model");
        currentMonsterSkin = FindMaterial(currentMonsterModel, "ironreaver01").GetComponent<SkinnedMeshRenderer>();
        originColor = currentMonsterSkin.materials[0].color;
        hitBurnCount = 0;
        burnStack = 0;
        burnDamage = 5;
        burnDuration = 5;
        burnElapsed = 0;
    }
    public IEnumerator MonsterSlow()
    {
        // 애니메이션도 슬로우적용되게 작성
        float prevSpeed = InitMonster.Instance.speedRun;
        InitMonster.Instance.speedRun -= (float)(InitMonster.Instance.speedRun * 0.3);
        while (isDamage)
        {
            currentMonsterSkin.materials[0].color = slowColor;
            isDamage = false;
            yield return new WaitForSecondsRealtime(10f);
        }
        isSlow = false;
        currentMonsterSkin.materials[0].color = originColor;
        InitMonster.Instance.speedRun = prevSpeed;
    }
    public IEnumerator MonsterBurn()
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
    public void DoCoroutine(string name)
    {
        StartCoroutine(name);
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
