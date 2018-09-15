using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterToPointMovement : BaseSpellMovement
{

    #region Variables

    protected Rigidbody _rigidbody;
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

    #region Move

    protected internal override void Move()
    {
        if (_isMain && _moving)
        {
            int index = _curPosIndex + 1;
            Vector3 _newPos;
            Vector3 _direction = TargetPosition - transform.position;
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
                _newPos = transform.position + _direction.normalized * BaseObject.Stats.CurrentSpeed * (float)GSC.timerTick / 1000f;
            _newPos.y = 0.5f;
            lock (moveUpdate)
            {
                moveUpdate.Add(index, new MovementUpdate(_newPos));
                _curPosIndex++;
                _currentLerpTime = 0f;
                SendDataUDP.SendMovePosition(ObjectType.spell, BaseObject.index, index, _newPos);
                MovementUpdate value;
                if (moveUpdate.TryGetValue(index, out value))
                    value.Sended();
                else
                    throw new Exception("Move send exception");
            }
        }
    }

    IEnumerator DestroyCoroutine()
    {
        while(true)
        {
            BaseObject.NetworkDestoy();
            yield return new WaitForSeconds(1);
        }
    }

    #endregion 

}

