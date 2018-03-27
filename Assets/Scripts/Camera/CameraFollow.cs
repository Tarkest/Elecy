using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private static float _playerHeight = 0.5f;
    private static Vector3 _targetPosition;
    private static bool _battleMod;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _battleMod = false;
    }

    void Update ()
    {
        if (_targetPosition == null)
            _targetPosition = ObjectManager.playerPos;
        else
            transform.position = _targetPosition + new Vector3(0, _playerHeight, 0);
	}
}
