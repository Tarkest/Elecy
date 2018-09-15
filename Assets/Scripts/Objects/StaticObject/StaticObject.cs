public class StaticObject : BaseObject, IStatsSpecifier<StaticObjectStats>
{

    public StaticObjectStats Stats
    {
        get
        {
            return mStats as StaticObjectStats;
        }
    }

    public new virtual void Init(int hp)
    {
        Stats.Init(this, hp);
    }
}
