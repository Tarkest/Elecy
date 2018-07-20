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
        animator = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (_playerStats.battleIsOn)
        {
            if (_playerStats.isDash <= 0 && _playerStats.isStucked <= 0 && _playerStats.isStunned <= 0)
                Move(h, v);

            if (_playerStats.isStunned <= 0 && _playerStats.isCasting <= 0)
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
            if (_playerStats.isCasting >= 0)
            {
                _playerStats.castUnsuccses = true;
                _playerStats.isCasting  = 0;
            }
        }
        else
        {
            animator.SetFloat("speed", 0);
        }

    }

    public void Turn()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(ObjectManagerOld.mousePosition.x, 0.5f, ObjectManagerOld.mousePosition.z) - transform.position);
        _playerRigidbody.MoveRotation(rot);
    }
}
