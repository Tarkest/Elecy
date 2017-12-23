using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dashspeed = 30f;
    public float dashlenght = 20f;

    Vector3 movement;
    Vector3 dash;
    Vector3 dashEnd;
    bool isDash;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLenght = 100f;
    float dashH = 0f;
    float dashV = 0f;
    float dashDis = 0f;
    

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Moving(h, v);
        Turning();
        Dashing(h, v);
    }

    void Moving (float h, float v)
    {
        if(!isDash)
        {
            movement.Set(h, 0, v);

            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    void Turning ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if(Physics.Raycast (camRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Dashing (float h, float v)
    {
        if(isDash)
        {
            isDash = false;
        }
        else
        {
            dashEnd = Vector3.zero;
            isDash = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            dashH = h;
            dashV = v;
            isDash = true;
            dash.Set(dashH, 0, dashV);
            Debug.Log(transform.position);
            dashEnd = transform.position + dash.normalized * dashlenght;
            Debug.Log(dashEnd);
        }
    }
}
