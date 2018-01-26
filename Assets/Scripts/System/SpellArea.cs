using UnityEngine;

public class SpellArea : MonoBehaviour {

	void FixedUpdate () {
        Vector3 areaPos = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        areaPos.y = 0.5f;
        transform.position = areaPos; 
    }
}
