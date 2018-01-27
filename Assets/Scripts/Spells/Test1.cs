using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour
{

    private SpellContainer spell;

    private void Start()
    {
        spell = gameObject.GetComponent<SpellContainer>();
        spell.SpellConteinerLoad();
    }

}
