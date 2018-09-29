using System;
using System.Collections;
using UnityEngine;

public abstract class PositionUpdate : BaseUpdate<Vector3>
{
    public float currentLerpTime = 0f;
    internal Rigidbody mRigidbody;

    #region Unity

    public override void Init(Vector3 value, BaseObject o)
    {
        base.Init(value, o);
        mRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (mObject.moving)
        {
            mRigidbody.MovePosition(Vector3.MoveTowards(transform.position, currentValue, mObject.CurrentMoveSpeed * 0.025f));
        }
    }

    #endregion

    protected override void SendStepBack()
    {
        currentLerpTime = 0f;
        SendDataUDP.SendPositionStepback(mObject.type, mObject.index, currentIndex);
    }

}

