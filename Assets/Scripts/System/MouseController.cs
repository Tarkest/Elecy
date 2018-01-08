using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject playerObject;
    public Vector3 mousePosition;
    private int _floorMask;
    private float _camRayLenght = 200f;

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
    }

	void Update ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, _camRayLenght, _floorMask))
        {
            mousePosition = floorHit.point - playerObject.transform.position;
            mousePosition.y = 0f;
        }
    }
}
