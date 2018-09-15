public class PlayerStats : BaseStats, IStatsMenuSpecifier<PlayerMenu>
{
    public int CurrentSN;
    public float CurrentMoveSpeed;
    public float CurrentAttackSpeed;

    public PlayerMenu Stats
    {
        get
        {
            return mStats as PlayerMenu;
        }
    }

    public void Init(BaseObject obj)
    {
        mInit(obj);
        CurrentSN = Stats.MaxSN;
        CurrentMoveSpeed = Stats.BaseMoveSpeed;
        CurrentAttackSpeed = Stats.BaseAttackSpeed;
    }

}
