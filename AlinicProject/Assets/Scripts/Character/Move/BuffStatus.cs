using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffStatus : MonoBehaviour
{
    public static BuffStatus instance;
    public List<BuffBase> onBuff = new List<BuffBase>();
    public ScrollRect scrollRect;
    public GameObject buffSrc;
    public SpawnParticle spawnParticle;
    int random;

    void Awake()
    {
        instance = this;
    }
    public float Buff(string type, float origin)
    {

        if(onBuff.Count > 0)
        {
            float tmep = 0f;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if(onBuff[i].type.Equals(type))
                    tmep += origin * onBuff[i].percentage;
            }
            return origin * tmep;
        }
        else
        {
            return origin;
        }
    }
    public void RandomBuffBody()
    {
        random = Random.Range(1,3);
        switch (random)
        {
            case 1:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Gun");
                break;

            case 2:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Magazine");
                break;
            case 3:
                Debug.Log("²Î");
                break;
        }
    }
    public void RandomBuffIcon()
    {
        if (random == 1)
        {
            random = Random.Range(1, 4);
        }
        else if(random == 2)
        {
            random = Random.Range(5, 8);
        }
        else
        {
            random = 99999;
        }
        switch (random)
        {
            case 1:
                UIManager.instance.SetBuffIcon("Corrosion", 20);
                spawnParticle.currentBullet = "Corrosion";
                GameManager.instance.additionalDamage = 1.3f;
                break;
            case 2:
                UIManager.instance.SetBuffIcon("Fire", 20);
                spawnParticle.currentBullet = "Fire";
                break;
            case 3:
                UIManager.instance.SetBuffIcon("Force", 20);
                spawnParticle.currentBullet = "Force";
                break;
            case 4:
                UIManager.instance.SetBuffIcon("Ice", 20);
                spawnParticle.currentBullet = "Ice";
                break;
            case 5:
                UIManager.instance.SetBuffIcon("Heal", 15);
                break;
            case 6:
                UIManager.instance.SetBuffIcon("Ice", 30);
                break;
            case 7:
                UIManager.instance.SetBuffIcon("Large", 40);
                break;
            case 8:
                UIManager.instance.SetBuffIcon("Light", 60);
                break;
            default:
                break;
        }
    }
}
