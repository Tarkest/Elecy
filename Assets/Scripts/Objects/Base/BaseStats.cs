using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStats : MonoBehaviour
{

    public List<Effect> Effects;
    public ScriptableObject stats;

    internal BaseObject baseObject;

    internal virtual void SetBaseStats(BaseObject obj)
    {
        Effects = new List<Effect>();
        baseObject = obj;
    }

    internal abstract void TakeDamage(int damage);


}

