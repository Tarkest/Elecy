using UnityEngine;

public class StaticObjectStats : BaseStats, IStatsMenuSpecifier<StaticObjectMenu>
{

    public StaticObjectMenu Stats
    {
        get
        {
            return mStats as StaticObjectMenu;
        }
    }

    public void Init(BaseObject obj, int hp)
    {
        base.mInit(obj);
        CurrentHP = Stats.MaxHP = hp;
    }

}
