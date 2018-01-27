using UnityEngine;

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
    [System.NonSerialized]
    public string targetType;

    public void SpellConteinerLoad()
    {
        spellHP = spellStats.spellHP;
        sunergyCost = spellStats.sunergyCost;
        damage = spellStats.damage;
        distance = spellStats.distance;
        speed = spellStats.speed;
        castTime = spellStats.castTime;
        castArea = spellStats.castArea;
        targetType = spellStats.targetType;
    }
}
