using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContainer : MonoBehaviour {

    public short spellHash;
    public List<GameObject> variation;

    public GameObject GetSpellVariation(short Hash)
    {
        for(int i = 1; i <= gameObject.transform.childCount; i++)
        {
            variation.Add(transform.GetChild(i - 1).gameObject);
        }
        foreach(GameObject child in variation)
        {
            if(child.GetComponent<SpellStats>().CheckHash(Hash))
            {
                return child;
            }
        }
        return null;
    }

    public bool CheckHash(short hash)
    {
        return (hash == spellHash);
    }
}
