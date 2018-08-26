using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 targetPosition;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if(Network.currentManager != null)
        {
            if(Network.currentManager.Players != null)
            {
                if (Network.currentManager.Players[ObjectManager.playerIndex] != null)
                {
                    targetPosition = Network.currentManager.Players[ObjectManager.playerIndex].transform.position;
                    transform.position = Vector3.Lerp(transform.position, (targetPosition + new Vector3(0, GSC.cam_target_height, 0)), 5f * Time.fixedDeltaTime);
                }
            }
        }
    }
}
