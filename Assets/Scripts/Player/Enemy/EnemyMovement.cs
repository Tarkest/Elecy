using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    Vector3 pos;
    Quaternion rot;

    void Update()
    {
        if(change)
        {
            change = false;
            gameObject.transform.position = pos;
            gameObject.transform.rotation = rot;
        }

    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        pos = position;
        rot = rotation;
        change = true;
    }
}
