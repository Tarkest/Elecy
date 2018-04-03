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
    public int isStunned = 0;
    [System.NonSerialized]
    public int isCasting = 0;
    [System.NonSerialized]
    public int isStucked = 0;
    [System.NonSerialized]
    public int isSylensed = 0;
    [System.NonSerialized]
    public int isDash = 0;
    [System.NonSerialized]
    public bool castSuccses = false;
    [System.NonSerialized]
    public bool castUnsuccses = false;
    [System.NonSerialized]
    public float playerMoveSpeed;
    [System.NonSerialized]
    public float playerAttackSpeed;
    [System.NonSerialized]
    public int playerBasicDefence;
    [System.NonSerialized]
    public int playerFireDefence;
    [System.NonSerialized]
    public int playerEarthDefence;
    [System.NonSerialized]
    public int playerWindDefence;
    [System.NonSerialized]
    public int playerWaterDefence;
    [System.NonSerialized]
    public bool battleIsOn = false;

    public List<Effect> Effects;

    public void SetStats(int maxHP, int maxSN, float moveSpeed, float attackSpeed, int basicDefence, int fireDefence, int earthDefence, int windDefence, int waterDefence)
    {
        playerMaxHP = playerCurrentHP = maxHP;
        playerMaxSN = playerCurrentSN = maxSN;
        playerMoveSpeed = moveSpeed;
        playerAttackSpeed = attackSpeed;
        playerBasicDefence = basicDefence;
        playerFireDefence = fireDefence;
        playerEarthDefence = earthDefence;
        playerWindDefence = windDefence;
        playerWaterDefence = waterDefence;
    }

    public void BattleStart()
    {
        battleIsOn = true;
    }

    private void Update()
    {
        foreach (Effect Effect in Effects)
        {
            if (Effect.InvokeEffect(Time.deltaTime, this))
                Effects.Remove(Effect);
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
