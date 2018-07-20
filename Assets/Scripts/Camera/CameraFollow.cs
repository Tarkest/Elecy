using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 _targetPosition;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (ObjectManagerOld.Player != null)
        {
            _targetPosition = ObjectManagerOld.playerPos;
            transform.position = Vector3.Lerp(transform.position, (_targetPosition + new Vector3(0, GSC.cam_target_height, 0)), 5f * Time.deltaTime);
        }
    }
}
