using UnityEngine;

public class SpellMovement_Bullet : MonoBehaviour {

    private Vector3 _spellPosition;
    private Vector3 _mousePosition;
    private Vector3 _target;
    private Vector3 _direction;
    private float _startTime;
    private float path;
    private SpellContainer _spell;
    public bool isDying = false;

	void Start () {
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _startTime = Time.time;
        _spellPosition = GetComponent<Transform>().position;
        _spell = gameObject.GetComponent<SpellContainer>();
        _spell.SpellConteinerLoad();
	}

    private void FixedUpdate()
    {
        _direction = ((_mousePosition - _spellPosition) / (_mousePosition - _spellPosition).magnitude);
        _target = _direction * _spell.distance + _spellPosition;
        _target = new Vector3(_target.x, _spellPosition.y, _target.z);
        path = _spell.speed * (Time.time - _startTime); 

        transform.position = Vector3.Lerp(_spellPosition, _target, path*Time.deltaTime);

        if (transform.position == _target)
            gameObject.GetComponent<AreaDamage>().Death();

    }


}
