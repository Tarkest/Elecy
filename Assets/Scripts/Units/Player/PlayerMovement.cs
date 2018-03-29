using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _playerRigidbody;

    private static bool moving = true;
    private static bool turning = true;

    public static bool Moving
    {
        get { return moving; }
    }

    public static bool Turning
    {
        get { return turning; }
    }

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {

        if (moving)
            Move();

        if (turning)
            Turn();

	}

    private void Move()
    {
        _playerRigidbody.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * GSC.speed * Time.deltaTime);
    }

    public void Turn()
    {
        Vector3 pos = ObjectManager.mousePosition;
        pos.y = GSC.player_Y;
        transform.LookAt(pos);
    }




}
