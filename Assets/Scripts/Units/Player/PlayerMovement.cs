using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _playerRigidbody;
    private PlayerStats _playerStats;
    private Animator animator;

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
        _playerStats = GetComponent<PlayerStats>();
        animator = GameObject.Find("TestPlayer0.1").GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (_playerStats.battleIsOn)
        {
            if (!_playerStats.isDash && !_playerStats.isStucked && !_playerStats.isStunned)
                Move(h, v);

            if (!_playerStats.isStunned && !_playerStats.isCasting)
                Turn();
        }
	}

    private void Move(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            animator.SetFloat("speed", 1f);
            Vector3 moving = Vector3.zero;
            moving.Set(h, 0, v);
            _playerRigidbody.MovePosition(transform.position + moving.normalized * _playerStats.playerMoveSpeed * Time.deltaTime);
            if (_playerStats.isCasting)
            {
                _playerStats.castUnsuccses = false;
                _playerStats.isCasting = false;
            }
        }
        else
        {
            animator.SetFloat("speed", 0);
        }

    }

    public void Turn()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(ObjectManager.mousePosition.x, 0, ObjectManager.mousePosition.z));
        _playerRigidbody.MoveRotation(rot);
    }




}
