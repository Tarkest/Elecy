using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 _targetPosition;
    private static bool _battleMod;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _battleMod = false;
    }

    void FixedUpdate()
    {
        if (ObjectManager.Player != null)
        {
            _targetPosition = ObjectManager.playerPos;
            transform.position = Vector3.Lerp(transform.position, (_targetPosition + new Vector3(0, GSC.cam_target_height, 0)), 5f * Time.deltaTime);
        }
    }
}
