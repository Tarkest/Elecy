using System;
using UnityEngine;

public abstract class PositionUpdate : BaseUpdate<Vector3>
{

    public float currentLerpTime = 0f;

    protected override void SendStepBack()
    {
        currentLerpTime = 0f;
        SendDataUDP.SendPositionStepback(mObject.type, mObject.index, currentIndex);
    }

}

