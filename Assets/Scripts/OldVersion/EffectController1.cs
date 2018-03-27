using UnityEngine.UI;
using UnityEngine;

public class EffectController1 : MonoBehaviour {

    public Effect1 effect;

    private bool _isStunned;
    private bool _isCast;
    private bool _isStucked;
    private bool _isStunStackable;
    private bool _isHpModStackable;
    private bool _isMsModStackable;

    private int _healthMod;
    private int _synergyMod;
    private int _damageMod;

    private float _movespeedMod;
    private float _stunduration;
    private float _currentstunduration;
    private float _stunDurationModifyer = 0f;
    private float _castduration;
    private float _stuckduration;
    private float _modFrequency;

    [System.NonSerialized]
    public Image buffIcon;

    private float _stunDurationCounter = 0f;
    private float _castDurationCounter = 0f;
    private float _stuckDurationCounter = 0f;

    public void EffectLoad () {
        _isStunned = effect.isStunning;
        _isCast = effect.isCasting;
        _isStucked = effect.isStucking;
        _isStunStackable = effect.isStunStackable;
        _isHpModStackable = effect.isHpModStackable;
        _isMsModStackable = effect.isMsModStackable;
        _healthMod = effect.healthMod;
        _synergyMod = effect.synergyMod;
        _damageMod = effect.damageMod;
        _movespeedMod = effect.movespeedMod;
        _stunduration = effect.stunduration;
        _castduration = effect.castduration;
        _stuckduration = effect.stuckduration;
        buffIcon = effect.buffIcon;
        _modFrequency = effect.modFrequency;
    }


    void Update()
    {
        if (_isStunned && _stunDurationModifyer < 6f)
        {
            _stunDurationCounter += Time.deltaTime;
            if (!GetComponent<PlayerStats1>().isStunned)
            {
                _stunDurationModifyer = 2f;
                _currentstunduration = _stunduration;
                GetComponent<PlayerStats1>().isStunned = true;
            }
            else if (GetComponent<PlayerStats1>().isStunned && _isStunStackable)
            {
                _currentstunduration += _stunduration / _stunDurationModifyer;
                _isStunStackable = false;
                _stunDurationModifyer *= 2f;
            }
            if (_stunDurationCounter >= _currentstunduration)
            {
                _isStunned = false;
                GetComponent<PlayerStats1>().isStunned = false;
                _stunDurationCounter = 0f;
                _stunDurationModifyer = 0f;
            }
        }

        if (_isCast && !GetComponent<PlayerStats1>().isCasting)
        {
            GetComponent<PlayerStats1>().castSuccses = false;
            GetComponent<PlayerStats1>().castUnsucces = false;
            GetComponent<PlayerStats1>().isCasting = true;
            _isCast = false;
        }
        else if (GetComponent<PlayerStats1>().isCasting && !GetComponent<PlayerStats1>().isStunned)
        {
            _castDurationCounter += Time.deltaTime;
            if (_castDurationCounter >= _castduration)
            {
                GetComponent<PlayerStats1>().castSuccses = true;
                _isCast = false;
            }
        }
        else
        {
            GetComponent<PlayerStats1>().castUnsucces = true;
            _isCast = false;
        }

        if (_isStucked && !GetComponent<PlayerStats1>().isStucked)
        {
            GetComponent<PlayerStats1>().isStucked = true;
        }
        else if (GetComponent<PlayerStats1>().isStucked)
        {
            _stuckDurationCounter += Time.deltaTime;
            if (_stuckDurationCounter >= _stuckduration)
            {
                _isStucked = false;
                GetComponent<PlayerStats1>().isStucked = false;
                _stuckDurationCounter = 0f;
            } 
        }
    }
}
