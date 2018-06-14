using UnityEngine;

public class DynamicProp : MonoBehaviour {

    public SpellHolder spell;
    private int _index;
    public Vector3 _position;
    public Quaternion _rotation;
    private Vector3 _serverPosition;
    private Quaternion _serverRotation;
    private bool _state;
    private int _hp;
    private int _currentHP;

    void Start()
    {
        _currentHP = _hp;    
    }

    void Update()
    {
        if (_state)
        {
            _position = gameObject.transform.position;
            _rotation = gameObject.transform.rotation;
            if (_currentHP <= 0)
            {
                Send Destroy 
            }
        }
    }

    public void SendInfo()
    {
        RoomSendData.SendDynamicObjectInfo(_index, _position, _rotation);
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void SetHp(int hp)
    {
        _hp = hp;
    }

    public void SetState(bool state)
    {
        _state = state;
    }

    public bool GetState()
    {
        return _state;
    }

    public void SetUpdate(Vector3 Position, Quaternion Rotation)
    {
        _serverPosition = Position;
        _serverRotation = Rotation;
    }

    public Vector3 GetServerPosition()
    {
        return _serverPosition;
    }

    public Quaternion GetServerRotation()
    {
        return _serverRotation;
    }

    public Vector3 GetPosition()
    {
        return _position;
    }

    public Quaternion GetRotation()
    {
        return _rotation;
    }
}
