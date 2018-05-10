using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProp : MonoBehaviour {

    public int _index { get; private set; }
    public int _hp { get; private set; }
    public int _currentHp { get; private set; }
    public int[] _effectsNet = new int[2];
    public bool _isActive { get; private set; }
    public Vector3 serverPos;
    public Quaternion serverRot;

    private List<Effect> _effects;

    void Update()
    {
        foreach(Effect effect in _effects)
        {
            if (effect.InvokeEffect(Time.deltaTime, this))
                _effects.Remove(effect);
        }
    }

    public void SendInfo()
    {
        RoomSendData.SendStaticObjectInfo(_index, _hp, _effectsNet);
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void SetHP(int HP)
    {
        _hp = HP;
    }

    public void UpdateHP(int HP)
    {
        _currentHp += HP; 
    }

    public void SetActivity(bool Activity)
    {
        _isActive = Activity;
    }

    public void SetTransform(Vector3 transform, Quaternion rotation)
    {
        serverPos = transform;
        serverRot = rotation;
    }

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }

    public void ChangeEffectIndex(int index)
    {
        if (_effectsNet[index] == 0)
            _effectsNet[index] = 1;
        else
            _effectsNet[index] = 0;
    }
}
