using UnityEngine;
using System.Collections.Generic;

public class Test1 : MonoBehaviour
{

    public List<GameObject> currentHitObjects;
    public float sphereRadius;
    public float maxDistance;

    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;
    private SpellContainer1 spell;

    private void Start()
    {
        spell = gameObject.GetComponent<SpellContainer1>();
        spell.SpellConteinerLoad();
    }

    private void OnTriggerEnter(Collider other)
    {
        spell.SpellConteinerLoad();
        origin = transform.position;
        direction = transform.forward;

        currentHitDistance = maxDistance;
        currentHitObjects.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(gameObject.transform.position, spell.castArea, transform.forward, 0f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject != gameObject)
            {
                currentHitObjects.Add(hit.transform.gameObject);
                currentHitDistance = hit.distance;
                ElementHit(hit.collider);
            }
        }
    }

    void ElementHit(Collider other)
    {
        if (other.tag == "background_element")
        {
            other.gameObject.GetComponent<Background_Stats>().currentHP -= spell.damage;
        }

        if (other.tag == "Player")
        {
            Debug.Log(other.gameObject.GetComponent<PlayerStats1>().playerCurrentHP);
            other.gameObject.GetComponent<PlayerStats1>().PlayerHealthPointUpdate(-(spell.damage));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance, Color.green);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, spell.castArea);
    }

}
