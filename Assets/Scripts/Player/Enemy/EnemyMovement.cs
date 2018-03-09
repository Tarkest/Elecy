using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    static bool start = false;
    Vector3 pos;
    Quaternion rot;
    static Vector3 startpos = new Vector3();
    static Quaternion startrot = new Quaternion();
    float startTime;
    float distance = 1f;
    float angle = 1f;
    float fracJourney;
    float speed = 20f;
    float rotSpeed = 0.2f;
    Timer timer;

    void FixedUpdate()
    {
        if (change)
        {
            change = false;
            startTime = Time.time;
        }

        if (start)
        {
            fracJourney = (Time.time - startTime) * speed;
            gameObject.transform.position = Vector3.Lerp(startpos, pos, fracJourney / distance);
            gameObject.transform.rotation = Quaternion.Lerp(startrot, rot, Time.deltaTime * rotSpeed);
        }
    }

    public static void SetStartPos(Vector3 position, Quaternion rotation)
    {
        startpos = position;
        startrot = rotation;
        start = true;
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        startpos = GlobalObjects.enemyPos;
        startrot = GlobalObjects.enemyRot;
        pos = position;
        rot = rotation;
        distance = Vector3.Distance(startpos, pos);
        //angle = Quaternion.Angle(startrot, rot);
        change = true;
    }
}
