using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 40f;
    public float dashLenght = 10f;
    public float dashCooldown = 3f;

    private float dashH = 0f;
    private float dashV = 0f;
    private float dashStartTime = 0f;
    private float dashJourneyLenght = 0f;
    private float dashJourey = 0f;
    private float dashDistCovered = 0f;
    private float dashCounter = 0f;

    private Vector3 movement;
    private Vector3 dash;
    private Vector3 dashStart;
    private Vector3 dashEnd;

    private Rigidbody playerRigidbody;

    private int impenetrableMask;

    private bool dashReady = true;
    private bool isDash;
    private bool isStunned = false;
    private bool isStucked = false;
    private bool isCasting = false;

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

    void Update()
    {
        isStunned = GetComponent<PlayerStats>().isStunned;
        isStucked = GetComponent<PlayerStats>().isStucked;
        isCasting = GetComponent<PlayerStats>().isCasting;
        DashCooldown();
    }

    void Moving (float h, float v)
    {
        if(!isDash && !isStunned && !isStucked)
        {
            movement.Set(h, 0, v);

            if (GetComponent<PlayerStats>().isCasting && h != 0 || v != 0)
                GetComponent<PlayerStats>().isCasting = false;

            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    void Turning ()
    {
        if (!isStunned)
        {
            Vector3 playerToMouse = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }

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
            if(dashReady && (h!=0f || v!=0f) && !isStunned && !isStucked)
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
                GetComponent<PlayerStats>().isCasting = false;
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
