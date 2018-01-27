using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

    public Spell spell;

    private Vector3 _casterPosition;

	void Start () {
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Background_Stats>().currentHP -= spell.damage;
        Destroy(gameObject);
    }
}
