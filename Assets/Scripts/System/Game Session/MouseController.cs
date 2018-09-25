using UnityEngine;

public class MouseController : MonoBehaviour {

    public static GameObject Object;
    public static Vector3 mousePosition;
    private static int _floorMask;
    private static float _camRayLenght = 200f;

    void Awake()
    {
        Object = gameObject;
        _floorMask = LayerMask.GetMask("Floor");
        mousePosition = MousePos(Input.mousePosition);
    }

	void Update ()
    {    
        transform.position = mousePosition = MousePos(Input.mousePosition);
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
