using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

    public Spell spell;

    private Vector3 _casterPosition;

	// Use this for initialization
	void Start () {
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
