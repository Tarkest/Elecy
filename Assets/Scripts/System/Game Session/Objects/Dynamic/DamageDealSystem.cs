using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealSystem : MonoBehaviour {

    private float _damageArea;
    private int _basicDamage;
    private int _fireDamage;
    private int _earthDamage;
    private int _windDamage;
    private int _waterDamage;

    void Awake()
    {
        int[] damage = gameObject.GetComponent<SpellHolder>().GetDamage();
        _basicDamage = damage[0];
        _fireDamage = damage[1];
        _earthDamage = damage[2];
        _windDamage = damage[3];
        _waterDamage = damage[4];
    }

    public bool DealSphereDamage()
    {
        RaycastHit[] elements = Physics.SphereCastAll(gameObject.transform.position, _damageArea, transform.forward, 0f);
        if(elements != null)
        {
            foreach(RaycastHit hit in elements)
            {
                DealDamage(hit.collider);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DealDamage(Collider hit)
    {
        try
        {
            hit.gameObject.GetComponent<PlayerStats>().PlayerDealDamage(_basicDamage, _fireDamage, _earthDamage, _windDamage, _waterDamage);
            return;
        }
        catch
        {
            try
            {
                hit.gameObject.GetComponent<DynamicProp>().UpdateHP(_fireDamage, _earthDamage, _windDamage, _waterDamage);
                return;
            }
            catch
            {
                try
                {
                    hit.gameObject.GetComponent<StaticProp>().UpdateHP(_basicDamage, _fireDamage, _earthDamage, _windDamage, _waterDamage);
                    return;
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
