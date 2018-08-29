using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerMovement : BaseMovement
{

    #region Variables

    protected Rigidbody _playerRigidbody;
    protected Animator animator;
    protected bool moving;

    #endregion

    #region Unity

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        moveUpdate = new Dictionary<int, MovementUpdate>();
    }

    void Update()
    {
        if (_isMain && moving)
        {
            MovementUpdate value;
            if (moveUpdate.TryGetValue(_curPosIndex, out value))
                _curPosition = value.position;
        }
    }

    void FixedUpdate ()
    {
        if (moving)
        {
            _currentLerpTime += Time.fixedDeltaTime;
            if (_currentLerpTime > (float)GSC.timerTick / 1000)
                _currentLerpTime = (float)GSC.timerTick / 1000;
            float _delta = _currentLerpTime * 1000 / (float)GSC.timerTick;
            _playerRigidbody.MovePosition(Vector3.Lerp(transform.position, _curPosition, _delta));
        }
	}

    #endregion

    #region Initialize

    protected internal override void SetMovement(BaseObject obj, bool isPlayer = false, params Vector3[] spawnPosition)
    {
        _isMain = isPlayer;
        baseObject = obj;
        if(_isMain)
        {
            _curPosIndex = 1;
            moveUpdate.Add(_curPosIndex, new MovementUpdate(spawnPosition[0]));
            MovementUpdate _value;
            if(moveUpdate.TryGetValue(_curPosIndex, out _value)) 
                _value.Received();
            else { throw new Exception("Set Movement Exception: There is no value in dictionary."); }
        }
        _curPosition = spawnPosition[0];
        moving = true;
    }

    #endregion

    #region Move

    protected internal override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            int index = _curPosIndex + 1;
            Vector3 newPosition;
            Vector3 direction = new Vector3(h, 0, v);
            RaycastHit _hit;
            if(Physics.Raycast(transform.position, direction.normalized, out _hit, Vector3.Distance(transform.position, _curPosition + direction.normalized * (baseObject.Stats as PlayerStats).CurrentMoveSpeed * (float)GSC.timerTick / 1000f)))
            {
                newPosition = _hit.point;

            }
            else
            {
                newPosition = _curPosition + direction.normalized * (baseObject.Stats as PlayerStats).CurrentMoveSpeed * (float)GSC.timerTick / 1000f;
            }
            newPosition.y = 0.5f;      
            lock(moveUpdate)
            {
                moveUpdate.Add(index, new MovementUpdate(newPosition));
                _curPosIndex++;
                _currentLerpTime = 0f;
                RoomUDPSendData.SendMovePosition(0, index, newPosition);
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
