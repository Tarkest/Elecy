﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    protected bool _isMain;
    protected Vector3 _curPosition;
    protected int _curPosIndex;
    protected float _currentLerpTime;

    internal Dictionary<int, MovementUpdate> moveUpdate;
    internal BaseObject baseObject;

    protected internal void CheckPosition(int index, float[] pos)
    {
        if (_isMain)
        {
            lock (moveUpdate)
            {
                MovementUpdate value;
                if (moveUpdate.TryGetValue(index, out value))
                {
                    if (!value.received)
                    {
                        if (!value.position.Equals(new Vector3(pos[0], 0.5f, pos[1])))
                        {
                            BattleLogic.StopTimer();
                            List<int> removeIndexes = new List<int>();
                            foreach (var k in moveUpdate.Reverse())
                            {
                                if (k.Value.received && k.Key < index)
                                {
                                    _curPosIndex = k.Key;
                                    _currentLerpTime = 0f;
                                    RoomUDPSendData.SendMoveBack(k.Key);
                                    break;
                                }
                                else
                                {
                                    removeIndexes.Add(k.Key);
                                }
                            }
                            foreach (int i in removeIndexes)
                            {
                                moveUpdate.Remove(i);
                            }
                            BattleLogic.StartTimer();
                        }
                        else
                        {
                            int[] _keys = moveUpdate.Keys.ToArray();
                            foreach (int key in _keys)
                            {
                                if (key < index)
                                    moveUpdate.Remove(key);
                            }
                            value.Received();
                        }
                    }
                }
            }
        }
        else
        {
            _curPosition.x = pos[0];
            _curPosition.z = pos[1];
            _currentLerpTime = 0f;
        }
    }

    protected internal abstract void SetMovement(BaseObject obj, bool isPlayer = false, params Vector3[] pos);
    protected internal abstract void Move();

}

internal struct MovementUpdate
{
    public readonly Vector3 position;
    public bool sent;
    public bool received;

    public MovementUpdate(Vector3 position)
    {
        this.position = position;
        sent = false;
        received = false;
    }

    public void Sended()
    {
        sent = true;
    }

    public void Received()
    {
        received = true;
    }
}



