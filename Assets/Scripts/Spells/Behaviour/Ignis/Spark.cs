using UnityEngine;

public class Spark : MonoBehaviour {

    private int spellType;
    private SpellContainer _spell;
    private Vector3 _casterPosition;

    //Spell Attack
    private Vector3 _spellPosition;
    private Vector3 _mousePosition;
    private Vector3 _target;
    private Vector3 _direction;
    private float _startTime;
    private float path;

    private void Start()
    {
        _spell = gameObject.GetComponent<SpellContainer>();
        _spell.SpellConteinerLoad();
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
        _spellPosition = transform.position;
        _startTime = Time.time;
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        spellType = GameObject.Find("Test player").GetComponent<SpellInvoker>().spellType;
    }

    private void FixedUpdate()
    {
        _direction = ((_mousePosition - _spellPosition) / (_mousePosition - _spellPosition).magnitude);
        path = _spell.speed * (Time.time - _startTime);
        Debug.Log(spellType);
        if (spellType == 1)
        {
            Debug.Log("Attack");
            _target = _direction * _spell.distance + _spellPosition;
            _target = new Vector3(_target.x, _spellPosition.y, _target.z);
            transform.position = Vector3.Lerp(_spellPosition, _target, path * Time.deltaTime);
            if (transform.position == _target)
                gameObject.GetComponent<AreaDamage>().Death();
        } else if (spellType ==2)
        {
            Debug.Log("Defence");
            _target = _direction * 2 + _spellPosition;
            _target = new Vector3(_target.x, _spellPosition.y, _target.z);
            transform.position = Vector3.Lerp(_spellPosition, _target, path * Time.deltaTime);
            if (transform.position == _target)
                gameObject.GetComponent<AreaDamage>().Death();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<AreaDamage>().Death();
    }



}
