using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dashspeed = 30f;
    public float dashlenght = 5f;

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
            if(dashDis < dashlenght && dashDis!=0f)
            {
                dash.Set(dashH, 0, dashV);

                dash = dash.normalized * dashspeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + dash);
                dashDis = Vector3.Distance(transform.position, dashEnd);
                Debug.Log(dashDis);
                Debug.Log(transform.position);
            }
            else
            {
                dashDis = 0f;
                isDash = false;
                Debug.Log(isDash);
            }
        }
        else 
        {
            if (Input.GetButtonDown("Jump"))
            {
                dashH = h;
                dashV = v;
                isDash = true;
                Debug.Log(isDash);
                dash.Set(dashH, 0, dashV);
                dashEnd = dash.normalized * dashlenght;
                Debug.Log(dashEnd);
            }
        }
    }
}
