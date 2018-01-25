using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement_Bullet : MonoBehaviour {

    private Vector3 _spellPosition;
    private Vector3 _target;
    private float _startTime;
    private float path;

    public float distance = 2f;
    public float speed = 50f;

	void Start () {
        _startTime = Time.time;
        _spellPosition = GetComponent<Transform>().position;
        _target = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition * distance; 
	}

    private void FixedUpdate()
    {
        path = speed * (Time.time - _startTime); 
        transform.position = Vector3.Lerp(_spellPosition, _target, path*Time.deltaTime);
        if (transform.position == _target)
            Destroy(gameObject, 1);
    }

}
