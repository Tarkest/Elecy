﻿using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Vector3 _serverPos;
    private Quaternion _serverRot;
    private float _positionDist;
    private Rigidbody _enemyRigidbody;

    void Awake()
    {
        _enemyRigidbody = gameObject.GetComponent<Rigidbody>();    
    }

    void Start ()
    {
        _serverPos = ObjectManagerOld.enemyPos;
        _serverRot = ObjectManagerOld.enemyRot;
	}
	
	void Update ()
    {
        _positionDist = Vector3.Distance(gameObject.transform.position, _serverPos);
        if(_positionDist > 0.1f && _positionDist < 10.5f)
        {
            _enemyRigidbody.MovePosition(Vector3.Lerp(ObjectManagerOld.enemyPos, _serverPos, 0.1f));
        } else
        {
            _enemyRigidbody.MovePosition(_serverPos);
        }
        _enemyRigidbody.MoveRotation(Quaternion.Lerp(ObjectManagerOld.enemyRot, _serverRot, 0.1f));
	}

    public void SetServerPosition(float[]Position, float[]Rotation)
    {
        _serverPos = new Vector3(Position[0], Position[1], Position[2]);
        _serverRot = new Quaternion(Rotation[0], Rotation[1], Rotation[2], Rotation[3]);
    }
}
