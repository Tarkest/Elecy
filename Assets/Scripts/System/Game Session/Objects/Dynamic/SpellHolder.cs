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
}
