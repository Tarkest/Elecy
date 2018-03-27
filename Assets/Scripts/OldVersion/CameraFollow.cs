using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    public float mouseOffsetMultiplier = 0.01f;
    public float mouseOffsetMultiplierMax = 1.5f;
    float mouseOffset;

    Vector3 offset;
    Vector3 offsetAngle;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = ObjectManager.Player.transform;
        }
        else
        {
            /*mouseOffset = Vector3.Distance(target.position, Input.mousePosition) / mouseOffsetMultiplier;
            if (mouseOffset > mouseOffsetMultiplierMax) mouseOffset = 1.5f;*/
            Vector3 targetCamPos = target.position /** mouseOffset*/ + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

}
