using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStats : MonoBehaviour, IHPOuterChange
{
    public BaseStatsMenu mStats;
    internal BaseObject baseObject;

    public int CurrentHP;
    public int currentHPIndex;
    protected Dictionary<int, HPUpdate> _HPUpdate;

    public int CurrentBaseDefence;
    public int CurrentBaseFireDefence;
    public int CurrentBaseEarthDefence;
    public int CurrentBaseWindDefence;
    public int CurrentBaseWaterDefence;

    protected void mInit(BaseObject obj)
    {
        _HPUpdate = new Dictionary<int, HPUpdate>();
        baseObject = obj;
        CurrentHP = mStats.MaxHP;

        CurrentBaseDefence = mStats.BaseNormalDefence;
        CurrentBaseFireDefence = mStats.BaseFireDefence;
        CurrentBaseEarthDefence = mStats.BaseEarthDefence;
        CurrentBaseWindDefence = mStats.BaseWindDefence;
        CurrentBaseWaterDefence = mStats.BaseWaterDefence;
    }

    public void HPUpdate()
    {

    }

    public virtual void HPOuterChange(int change)
    {

    }

    protected internal void HPCheck()
    {

    }
}

public struct HPUpdate
{
    int hp;
    bool sent;
    bool received;

    public HPUpdate(int hp)
    {
        this.hp = hp;
        sent = false;
        received = false;
    }

    public void Sent()
    {
        sent = true;
    }

    public void Received()
    {
        received = true;
    }
}

