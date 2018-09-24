using UnityEngine;

public abstract class RotationUpdate : BaseUpdate<Quaternion>
{

    public override void Callback()
    {
        lock(locker)
        {
            if(!transform.rotation.Equals(currentValue))
            {
                currentIndex++;
                currentValue = transform.rotation;
                updateLibrary.Add(currentIndex, new UpdateContainer<Quaternion>(currentValue));
                SendDataUDP.SendRotationUpdate(mObject.type, mObject.index, currentIndex, currentValue);
            }
        }

    }

    protected override void SendStepBack()
    {
        SendDataUDP.SendRotationStepback(mObject.type, mObject.index, currentIndex);
    }

}





