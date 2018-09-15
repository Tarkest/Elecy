using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject, IStatsSpecifier<PlayerStats>, IMovementSpecifier<BaseMovement>
{

    #region Variables

    public string nickname;
    public BaseInvoker PlayerInvoker;

    public PlayerStats Stats
    {
        get
        {
            return mStats as PlayerStats;
        }
    }
    public BaseMovement Movement
    {
        get
        {
            return mMovement as BaseMovement;
        }
    }

    #endregion

    #region Public Commands

    public virtual void Init(int index, string nickname, Vector3 pos, Quaternion rot, bool isPlayer = false)
    {
        base.Init(index);
        this.nickname = nickname;
        Stats.Init(this);
        Movement.Init(this, isPlayer, pos);
    }

    public virtual void LoadCombinations(List<GameObject> spells)
    {
        PlayerInvoker.Init(this, spells);
    }

    public virtual Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    #endregion

}
