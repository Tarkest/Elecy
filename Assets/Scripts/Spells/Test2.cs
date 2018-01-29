using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

    private SpellContainer _spell;
    private Vector3 _casterPosition;

	void Start () {
        _spell = gameObject.GetComponent<SpellContainer>();
        _spell.SpellConteinerLoad();
        _casterPosition = GameObject.Find("CastPoint").GetComponent<Transform>().position;
        transform.position = _casterPosition;
	}

}
