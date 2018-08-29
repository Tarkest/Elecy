using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//public class Spark : Behaviour
//{
    //private Vector3 _startPosition;
    //private Vector3 _targetPosition;

    //public override void InvokeBehaviour()
    //{
    //    _startPosition = casterPosition;
    //    _targetPosition = mousePosition;
    //}
    
    //new void Update()
    //{
    //    base.Update();
    //    if (transform.position.Equals(_targetPosition))
    //        gameObject.GetComponent<NetworkObjectController>().NetworkDestoy();
    //}

    //public override void Move()
    //{
    //    if(GetComponent<NetworkObjectController>().owner)
    //    {
    //        int index = _curPosIndex+1;
    //        Vector3 _newPos;
    //        _newPos = Vector3.RotateTowards(transform.position, _targetPosition, 0f, 0f);
    //        _newPos = transform.position + _newPos.normalized * GetComponent<NetworkObjectController>().spell.speed * (float)GSC.timerTick / 1000f;
    //        _newPos.y = 0.5f;
    //        lock (_moveUpdate)
    //        {
    //            _moveUpdate.Add(index, new MovementUpdate(_newPos));
    //            _curPosIndex++;
    //            _currentLerpTime = 0f;
    //            RoomUDPSendData.SendMovePosition(GetComponent<NetworkObjectController>().index, index, _newPos);
    //            MovementUpdate value;
    //            if (_moveUpdate.TryGetValue(index, out value))
    //                value.Sended();
    //            else
    //                throw new Exception("Move send exception");
    //        }
    //    }
    //}
//}