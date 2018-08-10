using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 targetPosition;
    public static int playerIndex;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (ObjectManager.players[playerIndex] != null)
        {
            targetPosition = ObjectManager.players[playerIndex].transform.position;
            transform.position = Vector3.Lerp(transform.position, (targetPosition + new Vector3(0, GSC.cam_target_height, 0)), 5f * Time.deltaTime);
        }
    }
}
