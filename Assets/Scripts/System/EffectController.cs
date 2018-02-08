using UnityEngine.UI;
using UnityEngine;

public class EffectController : MonoBehaviour {

    public Effect effect;

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
            if (!GetComponent<PlayerStats>().isStunned)
            {
                _stunDurationModifyer = 2f;
                _currentstunduration = _stunduration;
                GetComponent<PlayerStats>().isStunned = true;
            }
            else if (GetComponent<PlayerStats>().isStunned && _isStunStackable)
            {
                _currentstunduration += _stunduration / _stunDurationModifyer;
                _isStunStackable = false;
                _stunDurationModifyer *= 2f;
            }
            if (_stunDurationCounter >= _currentstunduration)
            {
                _isStunned = false;
                GetComponent<PlayerStats>().isStunned = false;
                _stunDurationCounter = 0f;
                _stunDurationModifyer = 0f;
            }
        }

        if (_isCast && !GetComponent<PlayerStats>().isCasting)
        {
            GetComponent<PlayerStats>().castSuccses = false;
            GetComponent<PlayerStats>().castUnsucces = false;
            GetComponent<PlayerStats>().isCasting = true;
            _isCast = false;
        }
        else if (GetComponent<PlayerStats>().isCasting && !GetComponent<PlayerStats>().isStunned)
        {
            _castDurationCounter += Time.deltaTime;
            if (_castDurationCounter >= _castduration)
            {
                GetComponent<PlayerStats>().castSuccses = true;
                _isCast = false;
            }
        }
        else
        {
            GetComponent<PlayerStats>().castUnsucces = true;
            _isCast = false;
        }

        if (_isStucked && !GetComponent<PlayerStats>().isStucked)
        {
            GetComponent<PlayerStats>().isStucked = true;
        }
        else if (GetComponent<PlayerStats>().isStucked)
        {
            _stuckDurationCounter += Time.deltaTime;
            if (_stuckDurationCounter >= _stuckduration)
            {
                _isStucked = false;
                GetComponent<PlayerStats>().isStucked = false;
                _stuckDurationCounter = 0f;
            } 
        }
    }
}
