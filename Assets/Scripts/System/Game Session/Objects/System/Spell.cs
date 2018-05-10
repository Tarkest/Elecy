using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{

    [Header("Movement")]
    public float distance;
    public float speed;
    public float castArea;
    [Space]
    [Header("Damage")]
    public int physicDamage;
    public int fireDamage;
    public int earthDamage;
    public int windDamage;
    public int waterDamage;
    [Space]
    [Header("SpellStats")]
    public int spellHP;
    public int synergyCost;
    public int synergyAdd;
    [Space]
    [Header("Time")]
    public float castTime;
    public float duration;
    [Space]
    [Header("Effets")]
    public Effect[] Effects = new Effect[GSC.SpellsEffectCapasity];
    [Space]
    [Header("Graphics")]
    public Image spellIcon;

}
