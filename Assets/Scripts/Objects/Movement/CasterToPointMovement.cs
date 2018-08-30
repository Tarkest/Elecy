using System;
using System.Collections.Generic;
using UnityEngine;

public class CasterToPointMovement : BaseMovement
{

    #region Variables

    protected Rigidbody _rigidbody;
    public Vector3 _targetPosition;
    public bool _moving;
    public bool Destroying;

    #endregion

    #region Unity's

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        moveUpdate = new Dictionary<int, MovementUpdate>();
    }

    private void Update()
    {
        if (_isMain && _moving)
        {
            MovementUpdate _value;
            if (moveUpdate.TryGetValue(_curPosIndex, out _value))
            {
                _curPosition = _value.position;
            }
        }
        if (transform.position.Equals(_targetPosition))
        {
            if(!Destroying)
            {
                Destroying = true;
                (baseObject as Spell).NetworkDestoy();
            }
        }

    }

    private void FixedUpdate()
    {
        _currentLerpTime += Time.fixedDeltaTime;
        if (_currentLerpTime > (float)GSC.timerTick / 1000)
            _currentLerpTime = (float)GSC.timerTick / 1000;
        float _delta = _currentLerpTime * 1000 / (float)GSC.timerTick;
        _rigidbody.MovePosition(Vector3.Lerp(transform.position, _curPosition, _delta));
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Set starts properties for moving. 
    /// </summary>
    /// <param name="obj">Component's parent</param>
    /// <param name="isPlayer">If true the calculation happanes in this movement</param>
    /// <param name="pos">Two Vector3: cast and target points</param>
    protected internal override void SetMovement(BaseObject obj, bool isPlayer = false, params Vector3[] pos)
    {
        baseObject = obj;
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
        _curPosition = pos[0];
        _targetPosition = pos[1];
        _moving = true;
    }

    #endregion

    #region Move

    protected internal override void Move()
    {
        if (_isMain && _moving)
        {
            int index = _curPosIndex + 1;
            Vector3 _newPos;
            Vector3 _direction = _targetPosition - transform.position;
            if (_direction.x < 1f && _direction.x > -1f && _direction.z > -1f && _direction.z < 1f)
            {
                if (_direction.Equals(Vector3.zero))
                    return;
               _newPos = _targetPosition;
            }
            else
                _newPos = transform.position + _direction.normalized * (baseObject.Stats as SpellStats).CurrentSpeed * (float)GSC.timerTick / 1000f;
            _newPos.y = 0.5f;
            lock (moveUpdate)
            {
                moveUpdate.Add(index, new MovementUpdate(_newPos));
                _curPosIndex++;
                _currentLerpTime = 0f;
                RoomUDPSendData.SendMovePosition(ObjectType.spell, baseObject.index, index, _newPos);
                MovementUpdate value;
                if (moveUpdate.TryGetValue(index, out value))
                    value.Sended();
                else
                    throw new Exception("Move send exception");
            }
        }
    }

    #endregion 

}

