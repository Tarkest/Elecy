using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private static Vector3 _curPosition;
    private Rigidbody _enemyRigidbody;
    bool moving;

    public void SetStats(Vector3 pos)
    {
        _curPosition = pos;
        moving = true;
    }

    void Start ()
    {
        _enemyRigidbody = gameObject.GetComponent<Rigidbody>();    
	}

    void FixedUpdate()
    {
        if(moving)
            _enemyRigidbody.MovePosition(Vector3.Lerp(transform.position, _curPosition, Time.fixedDeltaTime));    
    }

    public static void UpdatePosition(float[] pos)
    {
        _curPosition.x = pos[0];
        _curPosition.z = pos[1];
    }

}
