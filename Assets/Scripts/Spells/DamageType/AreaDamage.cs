using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    SpellContainer _spell;

    void Start()
    {
        _spell = gameObject.GetComponent<SpellContainer>();
        _spell.SpellConteinerLoad();
    }

    private void OnTriggerEnter(Collider other)
    {
        Death();
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
            other.gameObject.GetComponent<PlayerStats>().PlayerHealthPointUpdate(-(_spell.damage));
            other.gameObject.GetComponent<EffectController>().effect = Resources.Load("Effects/Slow") as Effect;
            other.gameObject.GetComponent<EffectController>().EffectLoad();
        }

        if (other.tag == "spell") { 
            if (other.gameObject.GetComponent<SpellContainer>().spellHP > _spell.spellHP)
            { 
                other.gameObject.GetComponent<SpellContainer>().spellHP -= _spell.spellHP;
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP < _spell.spellHP)
            {
                _spell.spellHP -= other.gameObject.GetComponent<SpellContainer>().spellHP;
            }
            else if (other.gameObject.GetComponent<SpellContainer>().spellHP == _spell.spellHP)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
