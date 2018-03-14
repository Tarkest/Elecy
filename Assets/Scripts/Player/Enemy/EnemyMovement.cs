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
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, servPos, 0.1f);
        gameObject.transform.rotation = rot;
    }

    void FixedUpdate()
    {


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
