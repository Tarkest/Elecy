using UnityEngine;

public class EnemyMovement1 : MonoBehaviour {

    //bool change = false;
    //static bool start = false;
    Vector3 servPos;
    Quaternion servRot;
    //static Vector3 transPos = new Vector3();
    //static Quaternion transrot = new Quaternion();
    //Vector3 prevPos;
    //float currentDistance;
    float posDistance;
    public float maxLerpDist = 10.5f;
    public float minLerpDist = 0.1f;

    void Update()
    {
        posDistance = Vector3.Distance(gameObject.transform.position, servPos);
        if (posDistance > minLerpDist && posDistance < maxLerpDist)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, servPos, 0.1f);
        else
            gameObject.transform.position = servPos;
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, servRot, 0.1f);
    }

    public static void SetStartPos(Vector3 position, Quaternion rotation)
    {
        //transPos = position;
        //transrot = rotation;
    }

    public void SetTransform(float[] position, float[] rotation)
    {
        //transPos = ObjectManager.enemyPos;
        //transrot = ObjectManager.enemyRot;
        servPos = new Vector3(position[0], position[1],position[2]);
        servRot = new Quaternion(rotation[0], rotation[1],rotation[2],rotation[3]);

        //change = true;
        //start = true;
    }
}
