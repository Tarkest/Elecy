using UnityEngine;

public class SpellContainer : MonoBehaviour {

    public short spellHash;

    public GameObject GetSpellVariation(short Hash)
    {
        SpellStats[] stats = transform.GetComponentsInChildren<SpellStats>();
        foreach(SpellStats stat in stats)
        {
            if (stat.CheckHash(Hash))
                return stat.gameObject;
        }
        return null;
    }

    public bool CheckHash(short hash)
    {
        return (hash == spellHash);
    }
}
