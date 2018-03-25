using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public Camera mainCamera;

    public Vector3 mousePosition; 

    

	void Awake ()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

    private void Update()
    {
        mousePosition = MouseController.mousePosition;
    }


}
