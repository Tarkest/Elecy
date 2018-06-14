using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProp : MonoBehaviour {

    public int _index { get; private set; }
    public int _hp { get; private set; }
    public int _currentHp { get; private set; }
    public List<int> _effectsNet;
    public bool _isActive { get; private set; }
    public Vector3 serverPos;
    public Quaternion serverRot;

    private int _lastHP;
    private List<int> _lastEffects;
    private bool _lastState;

    private List<Effect> _effects;

    void Update()
    {
        foreach(Effect effect in _effects)
        {
            if (effect.InvokeEffect(Time.deltaTime, this))
            {
                _effects.Remove(effect);
                _effectsNet.Remove(_effects.IndexOf(effect));
            }         
        }
    }

    public void SendInfo()
    {
        RoomSendData.SendStaticObjectInfo(_index, _currentHp, _effectsNet);
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

        if(_isActive == true)
        {
            RoomSendData.SendStaticObjectInfo(_index, _currentHp, _effectsNet, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    public void SetTransform(Vector3 transform, Quaternion rotation)
    {
        serverPos = transform;
        serverRot = rotation;
    }

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
        _effectsNet.Add(effect.effectIndex);
    }

    public void ChangeEffectIndex(int index, int type)
    {
        _effectsNet[index] = type;
    }

    public bool CheckChange()
    {
        if(_lastEffects != _effectsNet || _currentHp != _lastHP || _lastState != _isActive)
        {
            _lastEffects = _effectsNet;
            _lastHP = _currentHp;
            _lastState = _isActive;
            return true;
        }
        else { return false; }
    }
}
