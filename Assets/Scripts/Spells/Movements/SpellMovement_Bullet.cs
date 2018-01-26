using UnityEngine;

public class SpellMovement_Bullet : MonoBehaviour {

    private Vector3 _spellPosition;
    private Vector3 _mousePosition;
    private Vector3 _target;
    private Vector3 _direction;
    private float _startTime;
    private float path;

    public Spell spell;

	void Start () {
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _startTime = Time.time;
        _spellPosition = GetComponent<Transform>().position;
	}

    private void FixedUpdate()
    {
        _direction = ((_mousePosition - _spellPosition) / (_mousePosition - _spellPosition).magnitude);
        _target = _direction * spell.distance + _spellPosition;
        path = spell.speed * (Time.time - _startTime); 

        transform.position = Vector3.Lerp(_spellPosition, _target, path*Time.deltaTime);

        if (transform.position == _target)
            Destroy(gameObject, 1);

    }


}
