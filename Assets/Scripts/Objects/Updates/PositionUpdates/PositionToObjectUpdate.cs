using System;
using System.Collections;
using UnityEngine;

public class PositionToObjectUpdate : PositionUpdate
{

    protected GameObject TargetObject;

    public void Init(Vector3 spawnPos, GameObject targetObj, BaseObject o)
    {
        base.Init(spawnPos, o);
        TargetObject = targetObj;
    }

    public override void Callback()
    {
        if (mObject.isMain && mObject.moving)
        {
            int index = currentIndex + 1;
            Vector3 _newPos;
            Vector3 _direction = TargetObject.transform.position - mObject.transform.position;
            if (_direction.x < 1f && _direction.x > -1f && _direction.z > -1f && _direction.z < 1f)
            {
                // _direction.Equals(Vector3.zero) includes magnitude and sqrMagnitude
                if (_direction.x == 0f && _direction.z == 0f)
                {
                    if (mObject.Destroying)
                    {
                        StartCoroutine(DestroyCoroutine());
                    }
                    return;
                }
                _newPos = TargetObject.transform.position;
            }
            else
                _newPos = transform.position + _direction.normalized * mObject.CurrentMoveSpeed * (float)GSC.timerTick / 1000f;
            _newPos.y = 0.5f;
            lock (locker)
            {
                startLerpPos = transform.position;
                updateLibrary.Add(index, new UpdateContainer<Vector3>(_newPos));
                currentIndex++;
                currentValue = _newPos;
                currentLerpTime = 0f;
                SendDataUDP.SendPositionUpdate(ObjectType.spell, mObject.index, index, _newPos);
                UpdateContainer<Vector3> value;
                if (updateLibrary.TryGetValue(index, out value))
                    value.Sent();
                else
                    throw new Exception("Move send exception");
                //Interpolate();
            }
        }
    }

    IEnumerator DestroyCoroutine()
    {
        while (true)
        {
            mObject.Destroy();
            yield return new WaitForSeconds(1);
        }
    }

}

