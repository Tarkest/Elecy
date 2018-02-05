﻿using UnityEngine;

public class SpellArea : MonoBehaviour {

    private float _spellArea;
    private float _spellRange;

    private string _targetType;

    private Vector3 _mousePosition;
    private Transform _playerPosition;

    private SpellContainer _spellContainer;

    private string _spellName;

    private ParticleSystem _psTarget;
    private ParticleSystem _psCircle;
    private ParticleSystem _psArrow;

    void Start()
    {
        _psTarget = GetComponent<ParticleSystem>();
        _psCircle = gameObject.transform.Find("SpellArea").GetComponent<ParticleSystem>();
        _psArrow = gameObject.transform.Find("SpellPointer").GetComponent<ParticleSystem>();
        _spellName = GameObject.Find("Test player").GetComponent<SpellInvoker>().spellName;
        _spellContainer = (Resources.Load(_spellName, typeof (GameObject)) as GameObject).GetComponent<SpellContainer>();
        _spellContainer.SpellConteinerLoad();
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _playerPosition = GameObject.Find("Test player").GetComponent<Transform>();
        _targetType = _spellContainer.targetType;
        _spellArea = _spellContainer.castArea;
        _spellRange = _spellContainer.distance;
    }

    void Update()
    {
        _spellName = GameObject.Find("Test player").GetComponent<SpellInvoker>().spellName;
        _spellContainer = (Resources.Load(_spellName, typeof(GameObject)) as GameObject).GetComponent<SpellContainer>();
        _spellContainer.SpellConteinerLoad();
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _mousePosition.y = 0.5f;
        _playerPosition = GameObject.Find("Test player").GetComponent<Transform>();
        _targetType = _spellContainer.targetType;
        _spellArea = _spellContainer.castArea;
        _spellRange = _spellContainer.distance;
    }

    void FixedUpdate()
    {
        transform.position = _playerPosition.position;
        var main = _psTarget.main;
        var circleMain = _psCircle.main;
        var arrowMain = _psArrow.main;



        main.startSize = _spellRange * 3f;
        _psCircle.GetComponent<Transform>().transform.position = Vector3.Lerp(_playerPosition.position, _mousePosition, _spellRange / (Vector3.Distance(_playerPosition.position, _mousePosition)));
        circleMain.startSize = _spellArea * 3f;
        _psArrow.GetComponent<Transform>().transform.position = Vector3.Lerp(_playerPosition.position, Vector3.Lerp(_playerPosition.position, _mousePosition, _spellRange / (Vector3.Distance(_playerPosition.position, _mousePosition))), 0.5f);
        arrowMain.startRotationZ = _playerPosition.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        _psArrow.GetComponent<Transform>().transform.rotation = _playerPosition.transform.rotation;
        arrowMain.startSizeX = arrowMain.startSizeY = Vector3.Distance(_playerPosition.position, Vector3.Lerp(_playerPosition.position, _mousePosition, _spellRange / (Vector3.Distance(_playerPosition.position, _mousePosition)))) * 1.4f;

        if (_targetType == "AreaR")
        {
            if(_psTarget.isStopped)
                _psTarget.Play();
            if (_psCircle.isStopped)
                _psCircle.Play();
            if (_psArrow.isPlaying)
                _psArrow.Stop();
        }
        if (_targetType == "ArrowR")
        {
            if (_psTarget.isStopped)
                _psTarget.Play();
            if (_psCircle.isPlaying)
                _psCircle.Stop();
            if (_psArrow.isStopped)
                _psArrow.Play();
        }
        if (_targetType == "ArrowNR")
        {
            if (_psTarget.isPlaying)
                _psTarget.Stop();
            if (_psCircle.isPlaying)
                _psCircle.Stop();
            if (_psArrow.isStopped)
                _psArrow.Play();
        }
        if (_targetType == "AreaNR")
        {
            if (_psTarget.isPlaying)
                _psTarget.Stop();
            if (_psCircle.isStopped)
                _psCircle.Play();
            if (_psArrow.isPlaying)
                _psArrow.Stop();
        }
        if (_targetType == "AreaOP")
        {
            _psCircle.GetComponent<Transform>().transform.position = _playerPosition.position;
            if (_psTarget.isPlaying)
                _psTarget.Stop();
            if (_psCircle.isStopped)
                _psCircle.Play();
            if (_psArrow.isPlaying)
                _psArrow.Stop();
        }

    }
}