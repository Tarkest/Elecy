using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [System.NonSerialized] 
    public int playerMaxHP = 100;
    [System.NonSerialized]
    public int playerMaxSN = 100;
    [System.NonSerialized]
    public int playerCurrentHP;
    [System.NonSerialized]
    public int playerCurrentSN;
    [System.NonSerialized]
    public bool isStunned = false;
    [System.NonSerialized]
    public bool isCasting = false;
    [System.NonSerialized]
    public bool isStucked = false;
    [System.NonSerialized]
    public bool isSylensed = false;
    [System.NonSerialized]
    public bool isDash = false;
    [System.NonSerialized]
    public bool castSuccses = false;
    [System.NonSerialized]
    public bool castUnsuccses = false;
    [System.NonSerialized]
    public float playerMoveSpeed;
    [System.NonSerialized]
    public float playerAttackSpeed;
    [System.NonSerialized]
    public bool battleIsOn = false;

    private List<Effect> Effects;

    public void SetStats(int maxHP, int maxSN, float moveSpeed, float attackSpeed)
    {
        playerMaxHP = playerCurrentHP = maxHP;
        playerMaxSN = playerCurrentSN = maxSN;
        playerMoveSpeed = moveSpeed;
        playerAttackSpeed = attackSpeed;
    }

    public void BattleStart()
    {
        battleIsOn = true;
    }

    private void Update()
    {
        foreach (Effect Effect in Effects)
        {

        }
    }

    public void PlayerSynergyUpdate(int x)
    {
        playerCurrentSN += x;
    }

    public void PlayerSynergyUpdate(float x, string type)
    {
        if (type == "Full")
            playerCurrentSN += Convert.ToInt32(((x / 100) * playerMaxSN));
        else if (type == "Current")
            playerCurrentSN += Convert.ToInt32(((x / 100) * playerCurrentSN));
    }

    public void PlayerHealthPointUpdate(int x)
    {
        playerCurrentHP += x;
    }

    public void PlayerHealthPointUpdate(float x, string type)
    {
        if (type == "Full")
            playerCurrentHP += Convert.ToInt32(((x / 100) * playerMaxHP));
        else if (type == "Current")
            playerCurrentHP += Convert.ToInt32(((x / 100) * playerCurrentHP));
    }
}
