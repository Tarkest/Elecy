using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContainer : MonoBehaviour {

    public short spellHash;
    public List<GameObject> variation;

    public GameObject GetSpellVariation(short Hash)
    {
        for(int i = 1; i <= gameObject.transform.childCount - 1; i++)
        {
            variation[i] = transform.GetChild(i).gameObject;
        }
        foreach(GameObject child in variation)
        {
            if(child.GetComponent<SpellProperties>().CheckHash(Hash))
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
