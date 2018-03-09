using System.Collections;
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
    float speed = 20f;

    void FixedUpdate()
    {
       if(change)
        {
            change = false;
            startTime = Time.time;
        }
       fracJourney = (Time.time - startTime) * speed;
       gameObject.transform.position = Vector3.Lerp(startpos, pos, fracJourney / distance);
       gameObject.transform.rotation = Quaternion.Lerp(startrot, rot, fracJourney / angle);
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        startpos = GlobalObjects.enemyPos;
        startrot = GlobalObjects.enemyRot;
        pos = position;
        rot = rotation;
        distance = Vector3.Distance(startpos, pos);
        angle = Quaternion.Angle(startrot, rot);
        change = true;
    }
}
