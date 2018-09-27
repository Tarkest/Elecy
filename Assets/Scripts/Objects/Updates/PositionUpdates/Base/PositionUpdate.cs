using System;
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
            currentLerpTime += Time.fixedDeltaTime;
            if (currentLerpTime > (float)GSC.timerTick / 1000)
                currentLerpTime = (float)GSC.timerTick / 1000;
            Debug.Log("Alpha: " + currentLerpTime);
            float _delta = currentLerpTime * 1000 / (float)GSC.timerTick;
            Debug.Log("Delta: " + _delta);
            mRigidbody.MovePosition(Vector3.Lerp(transform.position, currentValue, _delta));
        }
    }

    #endregion

    protected override void SendStepBack()
    {
        currentLerpTime = 0f;
        SendDataUDP.SendPositionStepback(mObject.type, mObject.index, currentIndex);
    }

}

