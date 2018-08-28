using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 targetPosition;
    public Transform player;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            targetPosition = player.position;
            transform.position = Vector3.Lerp(transform.position, (targetPosition + new Vector3(0, GSC.cam_target_height, 0)), 5f * Time.fixedDeltaTime);
        }
    }
}
