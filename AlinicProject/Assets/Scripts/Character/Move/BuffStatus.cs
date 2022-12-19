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
    public PlayerBody player;
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
    public void RandomBuffBody(int rInt)
    {
        random = rInt;

        switch (random)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Gun");
                break;

            case 5:
            case 6:
            case 7:
            case 8:
                UIManager.instance.imageBuffPanel.gameObject.SetActive(true);
                UIManager.instance.SetBuffBody("Magazine");
                break;
            default:
                Debug.Log("²Î");
                break;
        }
    }
    public void RandomBuffIcon(int rInt)
    {
        


        random = rInt;

        switch (random)
        {
            case 1: // ºÎ½ÄÅº
                UIManager.instance.SetBuffIcon("Corrosion", 20);
                spawnParticle.currentBullet = "Corrosion";
                GameManager.instance.additionalDamage = 1.3f;
                break;
            case 2: // È­¿°Åº
                UIManager.instance.SetBuffIcon("Fire", 20);
                spawnParticle.currentBullet = "Fire";
                break;
            case 3: // ¹«·ÂÅº
                UIManager.instance.SetBuffIcon("Force", 20);
                spawnParticle.currentBullet = "Force";
                break;
            case 4: // ºù°áÅº
                UIManager.instance.SetBuffIcon("Ice", 20);
                spawnParticle.currentBullet = "Ice";
                break;
            case 5: // ÀÀ±ÞÄ¡·áÅº
                UIManager.instance.SetBuffIcon("Heal", 15);
                player.DoCoroutine("Heal");
                break;
            case 6: // °ú³Ã°¢Åº
                UIManager.instance.SetBuffIcon("Ice", 30);
                ApplyBuff.instance.DoCoroutine("BuffIceMagazine");
                break;
            case 7: // ´ë¿ë·® ÅºÃ¢
                UIManager.instance.SetBuffIcon("Large", 40);
                ApplyBuff.instance.DoCoroutine("BuffLargeMagazine");
                break;
            case 8: // °æ·®È­ ÅºÃ¢
                UIManager.instance.SetBuffIcon("Light", 60);
                ApplyBuff.instance.DoCoroutine("BuffLightMagazine");
                break;
            default:
                break;
        }
    }
    
}
