using UnityEngine;

public class DynamicProp : MonoBehaviour {

    public Spell spell;
    private int _index;
    private Vector3 _position;
    private Quaternion _rotation;
    private string _state;
    private int _hp;
    private int _currentHP;

    void Start()
    {
        _currentHP = _hp;    
    }

    void Update()
    {
        _position = gameObject.transform.position;
        _rotation = gameObject.transform.rotation;
        if(_currentHP <= 0)
        {
            //Send Destroy
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
}
