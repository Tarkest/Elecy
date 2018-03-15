using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    bool change = false;
    static bool start = false;
    Vector3 servPos;
    Quaternion servRot;
    static Vector3 transPos = new Vector3();
    static Quaternion transrot = new Quaternion();
    Vector3 prevPos;
    float currentDistance;
    float posDistance;

    void Update()
    {
        posDistance = Vector3.Distance(gameObject.transform.position, servPos);
        if (posDistance > 0.1f && posDistance < 10f)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, servPos, 0.1f);
        else
            gameObject.transform.position = servPos;
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, servRot, 0.1f);
    }

    public static void SetStartPos(Vector3 position, Quaternion rotation)
    {
        transPos = position;
        transrot = rotation;
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        transPos = GlobalObjects.enemyPos;
        transrot = GlobalObjects.enemyRot;
        servPos = position;
        servRot = rotation;

        change = true;
        start = true;
    }
}
