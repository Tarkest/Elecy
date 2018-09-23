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
        int damage = gameObject.GetComponent<Spell>().Stats.Damage;
        SendDataTCP.SendDamage(type, index, damage);
    }
}