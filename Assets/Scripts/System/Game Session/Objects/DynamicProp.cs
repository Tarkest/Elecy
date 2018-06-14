using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicProp : MonoBehaviour {

    public Spell spell;
    private int _number;
    private Vector3 _position;
    private Quaternion _rotation;
    private string _state;
    private int _hp;
    private int _damage;

    void Update()
    {
        _position = gameObject.transform.position;
        _rotation = gameObject.transform.rotation;
    }

    public void SendInfo()
    {
        RoomSendData.SendDynamicObjectInfo(_number, _position, _rotation, _state, _hp, _damage);
    }
}
