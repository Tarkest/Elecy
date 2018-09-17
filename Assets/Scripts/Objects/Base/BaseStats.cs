using UnityEngine;

public abstract class BaseStats : MonoBehaviour, IHPOuterChange
{
    public BaseStatsMenu mStats;
    internal BaseObject baseObject;

    public int CurrentHP;
    public int CurrentBaseDefence;
    public int CurrentBaseFireDefence;
    public int CurrentBaseEarthDefence;
    public int CurrentBaseWindDefence;
    public int CurrentBaseWaterDefence;

    protected void mInit(BaseObject obj)
    {
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

    protected internal void HPInnerChange()
    {

    }
}

