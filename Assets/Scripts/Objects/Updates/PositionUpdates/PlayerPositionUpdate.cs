using System;
using UnityEngine;

public class PlayerPositionUpdate : PositionUpdate, IBaseObjectSpecifier<Player>
{

    public Player BaseObject
    {
        get
        {
            return mObject as Player;
        }
    }

     void FixedUpdate()
    {
        if (mObject.moving)
        {
            mRigidbody.MovePosition(Vector3.MoveTowards(transform.position, currentValue, BaseObject.CurrentMoveSpeed * 0.025f));
        }
    }

    public override void Callback()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if ((h != 0 || v != 0) && BaseObject.moving)
        {
            int index = currentIndex + 1;
            Vector3 newPosition;
            Vector3 direction = new Vector3(h, 0, v);
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, direction.normalized, out _hit, Vector3.Distance(transform.position, currentValue + direction.normalized * BaseObject.CurrentMoveSpeed * (float)GSC.timerTick / 1000f)))
            {
                newPosition = _hit.point;

            }
            else
            {
                newPosition = currentValue + direction.normalized * BaseObject.CurrentMoveSpeed * (float)GSC.timerTick / 1000f;
            }
            newPosition.y = 0.5f;
            lock (locker)
            {
                updateLibrary.Add(index, new UpdateContainer<Vector3>(newPosition));
                currentIndex++; // May cause problems
                currentValue = newPosition;
                currentLerpTime = 0f;
                SendDataUDP.SendPositionUpdate(ObjectType.player, BaseObject.index, index, newPosition);
                UpdateContainer<Vector3> value;
                if (updateLibrary.TryGetValue(index, out value))
                    value.Sent();
                else
                    throw new Exception("Move send exception");
                //Interpolate();
            }
        }
    }

}

