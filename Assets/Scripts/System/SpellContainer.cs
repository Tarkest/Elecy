﻿using UnityEngine;

public class SpellContainer : MonoBehaviour {

    public Spell spellStats;

    [System.NonSerialized]
    public int spellHP;
    [System.NonSerialized]
    public int sunergyCost;
    [System.NonSerialized]
    public int damage;
    [System.NonSerialized]
    public float distance;
    [System.NonSerialized]
    public float speed;
    [System.NonSerialized]
    public float castTime;
    [System.NonSerialized]
    public float castArea;

    public void SpellConteinerLoad()
    {
        spellHP = spellStats.spellHP;
        sunergyCost = spellStats.sunergyCost;
        damage = spellStats.damage;
        distance = spellStats.distance;
        speed = spellStats.speed;
        castTime = spellStats.castTime;
        castArea = spellStats.castArea;
    }

    public void SpellConteinerSave() {
        spellStats.spellHP = spellHP;
        spellStats.sunergyCost = sunergyCost;
        spellStats.damage = damage;
        spellStats.distance = distance;
        spellStats.speed = speed;
        spellStats.castTime = castTime;
        spellStats.castArea = castArea;
    }
}
