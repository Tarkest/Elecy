using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    private Rigidbody _playerRigidbody;
    private static PlayerStats _playerStats;
    private Animator animator;

    private Vector3 curPosition;
    private int curPosIndex;
    private Dictionary<int, MovementUpdate> _moveUpdate;

    bool isPlayer;
    bool moving;

    #endregion

    #region Unity

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        _moveUpdate = new Dictionary<int, MovementUpdate>();
    }

    void Update()
    {
        curPosition = _moveUpdate[curPosIndex].position;
    }

    void FixedUpdate ()
    {
        if(moving)
            _playerRigidbody.MovePosition(Vector3.Lerp(transform.position, curPosition, Time.fixedDeltaTime));
	}

    #endregion

    #region Initialize

    public void SetStats(Vector3 spawnPosition, bool isPlayer = false)
    {
        this.isPlayer = isPlayer;
        if(isPlayer)
        {
            curPosIndex = 0;
            _moveUpdate.Add(curPosIndex, new MovementUpdate(spawnPosition));
        }
        curPosition = spawnPosition;
        moving = true;
    }

    #endregion


    public void Move()
    {
        int index = ++curPosIndex;
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 newPosition = curPosition + direction.normalized * _playerStats.playerMoveSpeed * (float)GSC.timerTick;
        _moveUpdate.Add(index, new MovementUpdate(newPosition));
        RoomUDPSendData.SendMovePosition(index, newPosition);
        _moveUpdate[index].Sended();
    }

    public void CheckPosition(int index, float[] pos)
    {
        if(isPlayer)
        {
            if(!_moveUpdate[index].received)
            {
                if(!_moveUpdate[index].position.Equals(pos))
                {
                    BattleLogic.StopTimer();
                    List<int> removeIndexes = new List<int>();
                    foreach(KeyValuePair<int, MovementUpdate> k in _moveUpdate.Reverse())
                    {
                        if(k.Value.received && k.Key < index)
                        {
                            curPosIndex = k.Key;
                            RoomUDPSendData.SendMoveBack(k.Key);
                            break;
                        }
                        else
                        {
                            removeIndexes.Add(k.Key);
                        }
                        foreach(int i in removeIndexes)
                        {
                            _moveUpdate.Remove(i);
                        }
                        BattleLogic.StartTimer();
                    }
                }
                else
                {
                    foreach(int key in _moveUpdate.Keys)
                    {
                        if (key < index)
                            _moveUpdate.Remove(key);
                    }
                    _moveUpdate[index].Received();
                }
            }
        }
        else
        {
            curPosition.x = pos[0];
            curPosition.z = pos[1];
        }
    }

    private struct MovementUpdate
    {
        public readonly Vector3 position;
        public bool sent;
        public bool received;

        public MovementUpdate(Vector3 position)
        {
            this.position = position;
            sent = false;
            received = false;
        }

        public void Sended()
        {
            sent = true;
        }

        public void Received()
        {
            received = true;
        }
    }

}
