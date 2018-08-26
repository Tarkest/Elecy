using UnityEngine;

public class Spark1 : MonoBehaviour {

    private bool spellType;
    private SpellContainer1 _spell;
    private Vector3 _casterPosition;

    //Spell Attack/Defense
    private Vector3 _spellPosition;
    private Vector3 _mousePosition;
    private Vector3 _target;
    private Vector3 _direction;
    private float _startTime;
    private float path;

    private void Start()
    {
        _spell = gameObject.GetComponent<SpellContainer1>();
        _spell.SpellConteinerLoad();
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
        _spellPosition = transform.position;
        _startTime = Time.time;
        _mousePosition = MouseController.mousePosition;
        spellType = GameObject.Find("Test player").GetComponent<SpellInvoker1>().spellType;
    }

    private void FixedUpdate()
    {
        _direction = ((_mousePosition - _spellPosition) / (_mousePosition - _spellPosition).magnitude);
        path = _spell.speed * (Time.time - _startTime);
        if (spellType)
        {
            _target = _direction * _spell.distance + _spellPosition;
            _target = new Vector3(_target.x, _spellPosition.y, _target.z);
            transform.position = Vector3.Lerp(_spellPosition, _target, path * Time.deltaTime);
            if (transform.position == _target)
                gameObject.GetComponent<Damage>().Death();
        }
        else
        {
            _target = _direction * 2 + _spellPosition;
            _target = new Vector3(_target.x, _spellPosition.y, _target.z);
            transform.position = Vector3.Lerp(_spellPosition, _target, path * Time.deltaTime);
            if (transform.position == _target)
                gameObject.GetComponent<Damage>().Death();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Damage>().Death();
    }



}
