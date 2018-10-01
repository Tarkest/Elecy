using System;
using System.Collections;
using UnityEngine;

public abstract class PositionUpdate : BaseUpdate<Vector3>
{
    public float currentLerpTime = 0f;
    protected Vector3 startLerpPos;
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
            float times = ((float)GSC.timerTick / 1000f) / Time.fixedDeltaTime;
            currentLerpTime += 1f / times;
            if (currentLerpTime > 1f)
                currentLerpTime = 1f;
            mRigidbody.MovePosition(Vector3.Lerp(startLerpPos, currentValue, currentLerpTime));
        }
    }

    #endregion

    protected override void SendStepBack()
    {
        currentLerpTime = 0f;
        SendDataUDP.SendPositionStepback(mObject.type, mObject.index, currentIndex);
    }

}

