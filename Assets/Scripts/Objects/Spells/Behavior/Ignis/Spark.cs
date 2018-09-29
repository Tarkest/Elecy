using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : BaseBehavior
{

    internal override void Invoke()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        Tags tag = StringToTag(other.tag);
        ObjectType type = ObjectType.player;
        switch(tag)
        {
            case Tags.Enemy:
                type = ObjectType.player;
                break;
            default:
                return;
        }
        int index = other.gameObject.GetComponent<BaseObject>().index;
        int _pD = gameObject.GetComponent<Spell>().Stats.PhisicDamage;
        int _iD = gameObject.GetComponent<Spell>().Stats.IgnisDamage;
        int _tD = gameObject.GetComponent<Spell>().Stats.TerraDamage;
        int _aD = gameObject.GetComponent<Spell>().Stats.AquaDamage;
        int _cD = gameObject.GetComponent<Spell>().Stats.CaeliDamage;
        int _puD = gameObject.GetComponent<Spell>().Stats.PureDamage;
        SendDataTCP.SendDamage(type, index, _pD, _iD, _tD, _aD, _cD, _puD);
    }
}