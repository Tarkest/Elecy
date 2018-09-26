﻿using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public PositionUpdate positionUpdate;
    public RotationUpdate rotationUpdate;
    public HPUpdate hpUpdate;
    public BaseStatsMenu mStats;
    public int index;
    public ObjectType type;
    public bool isMain;
    public bool moving;
    internal Rigidbody mRigidbody;
    protected bool initiaziled;
    public bool Destroying;

    #region Stats

    public float CurrentMoveSpeed;

    #endregion

    #region Initialization

    protected void Init(int index, ObjectType type)
    {
        this.index = index;
        this.type = type;
        hpUpdate = gameObject.AddComponent<HPUpdate>();
        mRigidbody = transform.GetComponent<Rigidbody>();
        SetStartStats();
    }

    #endregion

    #region Tick Callback

    public virtual void Callback()
    {
        if(initiaziled)
        {
            positionUpdate.Callback();
        }
    }

    #endregion

    public virtual void Destroy()
    {
        if(Destroying)
            Destroy(gameObject);
        else
        {
            Destroying = true;
            SendDataTCP.SendDestroy(type, index);
        }

    }

    #region Private Helpers

    protected void SetStartStats()
    {
        CurrentMoveSpeed = mStats.BaseMoveSpeed;
    }

    #endregion

}
