using UnityEngine;

public class Spell : BaseObject, IStatsSpecifier<SpellStats>, IMovementSpecifier<BaseSpellMovement>
{

    #region Public Members

    public SpellStats Stats
    {
        get
        {
            return mStats as SpellStats;
        }
    }

    public BaseSpellMovement Movement
    {
        get
        {
            return mMovement as BaseSpellMovement;
        }
    }

    #endregion

    #region Init

    public void Init(Vector3 castPos, Vector3 targetPos, int index, bool isMain = false)
    {
        base.Init(index);
        Stats.Init(this);
        Movement.Init(this, isMain, targetPos, castPos);
    }

    #endregion

    public void NetworkDestoy()
    {
        SendDataTCP.SendDestroy(index);
    }
}

