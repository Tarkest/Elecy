using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAreaPosition : MonoBehaviour {


	void FixedUpdate () {
        Vector3 areaPos = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        areaPos.y = 0.5f;
        transform.position = areaPos; 
    }
}
