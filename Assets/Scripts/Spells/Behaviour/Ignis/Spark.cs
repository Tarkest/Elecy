using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : Behaviour {

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    public void SetStartPosition(Vector3 StartPos)
    {
        _startPosition = StartPos;
    }

    public void SetTargetPosition(Vector3 TargetPos)
    {
        _targetPosition = TargetPos;
    }

    public override void Move()
    {

    }
}
