using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    private void Awake()
    {
        instance = this;
    }
    public Player player;
    public float originalAtk = 100;
    public float originalDef = 20;

    void Start()
    {
        player.Atk = originalAtk;
        player.Def = originalDef;
    }


}
