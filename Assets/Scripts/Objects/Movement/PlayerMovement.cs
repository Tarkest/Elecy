using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerMovement : BaseMovement, IBaseObjectSpecifier<Player>
{

    #region Variables

    protected Rigidbody _playerRigidbody;
    protected Animator animator;

    public Player BaseObject
    {
        get
        {
            return _baseObject as Player;
        }
    }

    #endregion

    #region Unity

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        moveUpdate = new Dictionary<int, MovementUpdate>();
    }

    void Update()
    {
        if (_isMain && _moving)
        {
            MovementUpdate value;
            if (moveUpdate.TryGetValue(_curPosIndex, out value))
                _curPosition = value.position;
        }
    }

    void FixedUpdate ()
    {
        if (_moving)
        {
            _currentLerpTime += Time.fixedDeltaTime;
            if (_currentLerpTime > (float)GSC.timerTick / 1000)
                _currentLerpTime = (float)GSC.timerTick / 1000;
            float _delta = _currentLerpTime * 1000 / (float)GSC.timerTick;
            _playerRigidbody.MovePosition(Vector3.Lerp(transform.position, _curPosition, _delta));
        }
	}

    #endregion

    #region Move

    protected internal override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if ((h != 0 || v != 0) && _moving)
        {
            int index = _curPosIndex + 1;
            Vector3 newPosition;
            Vector3 direction = new Vector3(h, 0, v);
            RaycastHit _hit;
            if(Physics.Raycast(transform.position, direction.normalized, out _hit, Vector3.Distance(transform.position, _curPosition + direction.normalized * BaseObject.Stats.CurrentMoveSpeed * (float)GSC.timerTick / 1000f)))
            {
                newPosition = _hit.point;

            }
            else
            {
                newPosition = _curPosition + direction.normalized * BaseObject.Stats.CurrentMoveSpeed * (float)GSC.timerTick / 1000f;
            }
            newPosition.y = 0.5f;      
            lock(moveUpdate)
            {
                moveUpdate.Add(index, new MovementUpdate(newPosition));
                _curPosIndex++;
                _currentLerpTime = 0f;
                SendDataUDP.SendMovePosition(ObjectType.player, BaseObject.index, index, newPosition);
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
