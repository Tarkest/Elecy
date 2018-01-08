﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 40f;
    public float dashLenght = 10f;
    public GameObject Fireball;

    Vector3 movement;
    Vector3 dash;
    Vector3 dashStart;
    Vector3 dashEnd;
    bool isDash;
    Rigidbody playerRigidbody;
    int impenetrableMask;
    float dashH = 0f;
    float dashV = 0f;
    float dashStartTime = 0f;
    float dashJourneyLenght = 0f;
    float dashJourey = 0f;
    float dashDistCovered = 0f;

    void Awake()
    {
        impenetrableMask = LayerMask.GetMask("Impenetrable");
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
        Vector3 playerToMouse = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;

        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        playerRigidbody.MoveRotation(newRotation);
    }

    void Dashing (float h, float v)
    {
        if(isDash)
        {
            if (dashJourey < 1)
            {
                dashDistCovered = (Time.time - dashStartTime) * dashSpeed;
                dashJourey = dashDistCovered / dashJourneyLenght;
                playerRigidbody.MovePosition(Vector3.Lerp(dashStart, dashEnd, dashJourey));
            }
            else
            {
                dashEnd = Vector3.zero;
                isDash = false;
                dashJourey = 0f;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            dashH = h;
            dashV = v;
            dashStart = transform.position;
            isDash = true;
            dash.Set(dashH, 0, dashV);
            dashEnd = transform.position + dash.normalized * dashLenght;
            dashStartTime = Time.time;
            dashJourneyLenght = Vector3.Distance(transform.position, dashEnd);
            Ray dashRay = new Ray(dashStart, dash.normalized);
            RaycastHit impenetrableHit;
            if (Physics.Raycast(dashRay, out impenetrableHit, dashLenght, impenetrableMask))
            {
                dashEnd = impenetrableHit.point;
            }
            else
            {
                dashEnd = transform.position + dash.normalized * dashLenght;
            }
        }
    }
}
