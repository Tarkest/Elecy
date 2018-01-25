using UnityEngine;

public class MouseController : MonoBehaviour {

    public Vector3 mousePosition;
    private int _floorMask;
    private float _camRayLenght = 200f;

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        mousePosition = MousePos(Input.mousePosition);
    }

	void Update ()
    {
        mousePosition = MousePos(Input.mousePosition);
    }

    Vector3 MousePos (Vector3 x)
    {
        Ray camRay = Camera.main.ScreenPointToRay(x);
        Vector3 _mousePos = new Vector3();

        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, _camRayLenght, _floorMask))
        {
            _mousePos = floorHit.point;
            _mousePos.y = 0f;
        }
        return _mousePos;
    }
}
