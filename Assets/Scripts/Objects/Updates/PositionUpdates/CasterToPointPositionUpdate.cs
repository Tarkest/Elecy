using System;
using System.Collections;
using UnityEngine;

public class CasterToPointPositionUpdate : SpellPositionUpdate
{

    public override void Callback()
    {
        if (BaseObject.isMain && BaseObject.moving)
        {
            int index = currentIndex + 1;
            Vector3 _newPos;
            Vector3 _direction = TargetPosition - BaseObject.transform.position;
            if (_direction.x < 1f && _direction.x > -1f && _direction.z > -1f && _direction.z < 1f)
            {
                // _direction.Equals(Vector3.zero) includes magnitude and sqrMagnitude
                if (_direction.x == 0f && _direction.z == 0f)
                {
                    if (!Destroying)
                    {
                        Destroying = true;
                        StartCoroutine(DestroyCoroutine());
                    }
                    return;
                }
                _newPos = TargetPosition;
            }
            else
                _newPos = transform.position + _direction.normalized * BaseObject.CurrentSpeed * (float)GSC.timerTick / 1000f;
            _newPos.y = 0.5f;
            lock (locker)
            {
                updateLibrary.Add(index, new UpdateContainer<Vector3>(_newPos));
                currentIndex++;
                currentValue = _newPos;
                currentLerpTime = 0f;
                SendDataUDP.SendMovePosition(ObjectType.spell, BaseObject.index, index, _newPos);
                UpdateContainer<Vector3> value;
                if (updateLibrary.TryGetValue(index, out value))
                    value.Sent();
                else
                    throw new Exception("Move send exception");
            }
        }
    }

    IEnumerator DestroyCoroutine()
    {
        while (true)
        {
            BaseObject.NetworkDestoy();
            yield return new WaitForSeconds(1);
        }
    }

}

