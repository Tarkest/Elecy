﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    
    Vector3 pos;
    Quaternion rot;
    Vector3 startpos = new Vector3();
    Quaternion startrot = new Quaternion();
    float startTime;
    float distance = 1f;
    float angle = 1f;
    float fracJourney;

    void FixedUpdate()
    {
        //change = false;
       fracJourney = (Time.time - startTime);
       gameObject.transform.position = pos;
       //gameObject.transform.rotation = Quaternion.Lerp(startrot, rot, fracJourney / angle);
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        startpos = gameObject.transform.position;
        startrot = gameObject.transform.rotation;
        pos = Vector3.Lerp(startpos, position, fracJourney / distance);
        rot = rotation;
        startTime = Time.time;
        distance = Vector3.Distance(startpos, pos);
        angle = Quaternion.Angle(startrot, rot);
        //change = true;
    }
}
