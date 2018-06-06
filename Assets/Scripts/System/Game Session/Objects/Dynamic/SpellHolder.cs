using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{

    public Spell ScriptableObject;

    void Awake()
    {
        gameObject.GetComponent<DynamicProp>().SetHp(ScriptableObject.spellHP);       
    }

    public int[] GetDamage()
    {
        int[] damage = new int[5];
        damage[0] = ScriptableObject.physicDamage;
        damage[1] = ScriptableObject.fireDamage;
        damage[2] = ScriptableObject.earthDamage;
        damage[3] = ScriptableObject.windDamage;
        damage[4] = ScriptableObject.waterDamage;
        return damage;
    }
}
