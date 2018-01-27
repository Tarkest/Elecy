using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

    private SpellContainer _spell;
    private Vector3 _casterPosition;

	void Start () {
        _spell = gameObject.GetComponent<SpellContainer>();
        _spell.SpellConteinerLoad();
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "background_element")
        {
            other.gameObject.GetComponent<Background_Stats>().currentHP -= _spell.damage;
        }

        if(other.tag != "spell")
            Destroy(gameObject);
        else
        {
            if (other.gameObject.GetComponent<SpellContainer>().spellHP > _spell.spellHP)
            {
                other.gameObject.GetComponent<SpellContainer>().spellHP -= _spell.spellHP;
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP < _spell.spellHP)
            {
                _spell.spellHP -= other.gameObject.GetComponent<SpellContainer>().spellHP;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP == _spell.spellHP)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
          
    }
}
