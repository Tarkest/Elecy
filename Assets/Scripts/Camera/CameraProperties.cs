using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour {

    private Transform _parent;

    void Start()
    {
        _parent = transform.parent;
    }

    void LateUpdate()
    {
        transform.position = _parent.position + _parent.forward * -1 * GSC.cam_dist + Vector3.up * GSC.cam_height;
        transform.LookAt(_parent);
    }
}
