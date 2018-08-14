using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProperties : MonoBehaviour {

    public short variationHash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CheckHash(short hash)
    {
        if(variationHash == hash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
