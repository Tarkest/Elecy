using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    
    Vector3 pos;
    Quaternion rot;
    Vector3 startpos;
    Quaternion startrot;
    float startTime;
    float distance;
    float angle;

    void FixedUpdate()
    {
        //change = false;
       float fracJourney = (Time.time - startTime);
       gameObject.transform.position = Vector3.Lerp(startpos, pos, fracJourney / distance);
       gameObject.transform.rotation = Quaternion.Lerp(startrot, rot, fracJourney / angle);
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        pos = position;
        rot = rotation;
        startpos = gameObject.transform.position;
        startrot = gameObject.transform.rotation;
        startTime = Time.time;
        distance = Vector3.Distance(startpos, pos);
        angle = Quaternion.Angle(startrot, rot);
        //change = true;
    }
}
