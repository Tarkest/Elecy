using UnityEngine;

public class Damage : MonoBehaviour
{
    SpellContainer1 _spell;

    void Start()
    {
        _spell = gameObject.GetComponent<SpellContainer1>();
        _spell.SpellConteinerLoad();
    }

    public void Death()
    {
        RaycastHit[] elements = Physics.SphereCastAll(gameObject.transform.position, _spell.castArea, transform.forward, 0f);
        foreach (RaycastHit hit in elements)
        {
            if (hit.transform.gameObject != gameObject)
            {
                ElementHit(hit.collider);
            }
        }
        Destroy(gameObject);

    }

    void ElementHit(Collider other)
    {
        if (other.tag == "background_element")
        {
            other.gameObject.GetComponent<Background_Stats>().currentHP -= _spell.damage;
        }

        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats1>().PlayerHealthPointUpdate(-(_spell.damage));
            other.gameObject.GetComponent<EffectController1>().effect = Resources.Load("Effects/Slow") as Effect1;
            other.gameObject.GetComponent<EffectController1>().EffectLoad();
        }

        if (other.tag == "spell") { 
            if (other.gameObject.GetComponent<SpellContainer1>().spellHP > _spell.spellHP)
            { 
                other.gameObject.GetComponent<SpellContainer1>().spellHP -= _spell.spellHP;
            }
            else if (other.gameObject.GetComponent<SpellContainer1>().spellHP < _spell.spellHP)
            {
                _spell.spellHP -= other.gameObject.GetComponent<SpellContainer1>().spellHP;
            }
            else if (other.gameObject.GetComponent<SpellContainer1>().spellHP == _spell.spellHP)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
