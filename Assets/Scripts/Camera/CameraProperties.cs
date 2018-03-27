using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour {

    private static float distance;
    private static float height;

    private Transform _parent;

    void Start()
    {
        distance = 20f;
        height = 14f;
        _parent = transform.parent;
    }

    void LateUpdate()
    {
        transform.position = _parent.position + _parent.forward * -1 * distance + Vector3.up * height;
        transform.LookAt(_parent);
    }

    public static void Set(float dis, float heig) // do animation using lerp
    {
        distance = dis;
        height = heig;
    }
}
