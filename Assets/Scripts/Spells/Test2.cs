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
        Death();
    }

    public void Death()
    {
        RaycastHit[] elements = Physics.SphereCastAll(gameObject.transform.position, _spell.castArea, transform.forward, 0f);
        foreach(RaycastHit hit in elements)
        {
            if (hit.transform.gameObject != gameObject)
            {
                ElementHit(hit.collider);
            }
        }
    }

    void ElementHit(Collider other)
    {
        if (other.tag == "background_element")
        {
            other.gameObject.GetComponent<Background_Stats>().currentHP -= _spell.damage;
        }

        if (other.tag == "Player")
        {
            Debug.Log(other.gameObject.GetComponent<PlayerStats>().playerCurrentHP);
            other.gameObject.GetComponent<PlayerStats>().PlayerHealthPointUpdate(-(_spell.damage));
        }

        if (other.tag != "spell")
            Destroy(gameObject);
        else
        {
            if (other.gameObject.GetComponent<SpellContainer>().spellHP > _spell.spellHP)
            {
                Destroy(gameObject);
                other.gameObject.GetComponent<SpellContainer>().spellHP -= _spell.spellHP;

                Debug.Log(other.gameObject.name + other.gameObject.GetComponent<SpellContainer>().spellHP);
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP < _spell.spellHP)
            {
                _spell.spellHP -= other.gameObject.GetComponent<SpellContainer>().spellHP;
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP == _spell.spellHP)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        } 
    }



}
