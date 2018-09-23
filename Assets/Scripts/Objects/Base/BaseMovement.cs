using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#region BaseMovement

public abstract class BaseMovement : MonoBehaviour, ICheckPosition
{
    protected bool _isMain;
    protected Vector3 _curPosition;
    protected BaseObject _baseObject;
    protected float _currentLerpTime;
    protected bool _moving;

    public Vector3 StartPosition;
    public int _curPosIndex;
    public Dictionary<int, MovementUpdate> moveUpdate;


    public void CheckPosition(int index, float[] pos)
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
                        if (!value.position.Equals(new Vector3(pos[0], pos[1], pos[2])))
                        {
                            BattleLogic.StopTimer();
                            List<int> removeIndexes = new List<int>();
                            foreach (var k in moveUpdate.Reverse())
                            {
                                if (k.Value.received && k.Key < index)
                                {
                                    _curPosIndex = k.Key;
                                    _currentLerpTime = 0f;
                                    SendDataUDP.SendMoveBack(k.Key);
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
            _curPosition.y = pos[1];
            _curPosition.z = pos[2];

            _currentLerpTime = 0f;
        }
    }

    public virtual void Init(BaseObject obj, bool isPlayer = false, params Vector3[] pos)
    {
        _baseObject = obj;
        _isMain = isPlayer;
        if (_isMain)
        {
            _curPosIndex = 1;
            moveUpdate.Add(_curPosIndex, new MovementUpdate(pos[0]));
            MovementUpdate _value;
            if (moveUpdate.TryGetValue(_curPosIndex, out _value))
                _value.Received();
            else { throw new Exception("Set Movement Exception: There is no value in dictionary."); }
        }
        _curPosition = StartPosition = pos[0];
        _moving = true;
    }

    protected internal abstract void Move();

}

#endregion

#region Spell Movement

public abstract class BaseSpellMovement : BaseMovement, IBaseObjectSpecifier<Spell>
{
    public Vector3 TargetPosition;

    public Spell BaseObject
    {
        get
        {
            return _baseObject as Spell;
        }
    }

    public override void Init(BaseObject obj, bool isPlayer = false, params Vector3[] pos)
    {
        base.Init(obj, isPlayer, pos);
        TargetPosition = pos[1];
    }
}

#endregion

public struct MovementUpdate
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

    public void Sent()
    {
        sent = true;
    }

    public void Received()
    {
        received = true;
    }
}



