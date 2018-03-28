using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject _targetPosition;
    private static bool _battleMod;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _battleMod = false;
    }

    void Update ()
    {
        //if (_targetPosition == null)
        //    _targetPosition = ObjectManager.playerPos;
        //else
            transform.position = _targetPosition.transform.position + new Vector3(0, GSC.cam_target_height, 0);
	}
}
