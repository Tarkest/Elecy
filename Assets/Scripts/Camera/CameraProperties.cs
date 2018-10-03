using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour {

    public Vector3 Velosity;
    public float smooth = 5f;
    private bool _set; 
    public Transform player
    {
        get
        {
            return Network.currentManager.Players[ObjectManager.playerIndex].transform;
        }
        set
        {
            transform.position = value.position + new Vector3(0, GSC.cam_target_height, -GSC.cam_dist) + Vector3.up * GSC.cam_height;
            transform.LookAt(value);
            _set = true;
        }
    }

    void FixedUpdate()
    {
        if (_set)
        {
            transform.position = Vector3.Lerp(transform.position, (player.position + new Vector3(0, GSC.cam_target_height, -GSC.cam_dist) + Vector3.up * GSC.cam_height), smooth * Time.fixedDeltaTime);
        }
    }
}
