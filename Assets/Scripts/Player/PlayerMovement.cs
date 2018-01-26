using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 40f;
    public float dashLenght = 10f;
    public float dashCooldown = 3f;

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
    bool dashReady = true;
    float dashCounter = 0f;
    string playerState;
    

    void Awake()
    {
        impenetrableMask = LayerMask.GetMask("Impenetrable");
        playerRigidbody = GetComponent<Rigidbody>();
        playerState = GetComponent<PlayerStats>().playerCurrentState;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if(!(playerState == "stun"))
        {
            Moving(h, v);
            Turning();
            Dashing(h, v);
        }

    }

    void Update()
    {
        playerState = GetComponent<PlayerStats>().playerCurrentState;
        DashCooldown();
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
        Vector3 playerToMouse = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition - transform.position;
        playerToMouse.y = 0f;

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
            if(dashReady && (h!=0f || v!=0f))
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
                dashReady = false;
            }
        }
    }

    void DashCooldown ()
    {
        if (!dashReady)
        {
            dashCounter += Time.deltaTime;
            Image dashIcon = GameObject.Find("DashCooldownIndicator").GetComponent<Image>();
            Text dashTimeCount = GameObject.Find("DashCooldownCounter").GetComponent<Text>();
            dashIcon.fillAmount = dashCounter / dashCooldown;
            dashTimeCount.text = (dashCooldown - dashCounter).ToString("f1");
            if (dashCounter >= dashCooldown)
            {
                dashReady = true;
                dashCounter = 0f;
                dashTimeCount.text = "";
            }

        }
    }
}
