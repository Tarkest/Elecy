using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    static bool start = false;
    Vector3 servPos;
    Quaternion rot;
    static Vector3 transPos = new Vector3();
    static Quaternion startrot = new Quaternion();
    Vector3 prevPos;
    float currentDistance;
    float realDistance;
    float progress;

    void Update()
    {
        if(prevPos == Vector3.zero)
        {
            prevPos = servPos;
        }
        realDistance = Vector3.Distance(prevPos, servPos);
        currentDistance = Vector3.Distance(transPos, servPos);
        if(realDistance != 0)
        {
            progress = currentDistance / realDistance;
        }
        prevPos = servPos;

        transPos = Vector3.Lerp(transPos, servPos, progress);
    }

    void FixedUpdate()
    {
        if (change)
        {
            change = false;
        }

        if (start)
        {

            gameObject.transform.position = transPos;
            gameObject.transform.rotation = rot;
        }
    }

    public static void SetStartPos(Vector3 position, Quaternion rotation)
    {
        transPos = position;
        startrot = rotation;
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        transPos = GlobalObjects.enemyPos;
        startrot = GlobalObjects.enemyRot;
        servPos = position;
        rot = rotation;

        //angle = Quaternion.Angle(startrot, rot);
        change = true;
        start = true;
    }
}
