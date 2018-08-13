using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    private Rigidbody _playerRigidbody;
    private static PlayerStats _playerStats;
    private Animator animator;

    private Vector3 curPosition;
    private int curPosIndex;
    private Dictionary<int, MovementUpdate> _moveUpdate = new Dictionary<int, MovementUpdate>();

    bool isPlayer;
    bool moving;
    private float _currentLerpTime = 0f;
    public int[] _keys;

    #endregion

    #region Unity

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        //_moveUpdate = new Dictionary<int, MovementUpdate>();
    }

    void Update()
    {
        if (isPlayer && moving)
        {
            MovementUpdate value;
            if (_moveUpdate.TryGetValue(curPosIndex, out value))
                curPosition = value.position;
        }
    }

    void FixedUpdate ()
    {
        if (moving)
        {
            _currentLerpTime += Time.fixedDeltaTime;
            if (_currentLerpTime > (float)GSC.timerTick / 1000)
                _currentLerpTime = (float)GSC.timerTick / 1000;
            float _delta = _currentLerpTime * 1000 / (float)GSC.timerTick;
            Debug.Log(_delta);
            _playerRigidbody.MovePosition(Vector3.Lerp(transform.position, curPosition, _delta));
        }
	}

    #endregion

    #region Initialize

    public void SetStats(Vector3 spawnPosition, bool isPlayer = false)
    {
        this.isPlayer = isPlayer;
        if(isPlayer)
        {
            curPosIndex = 1;
            _moveUpdate.Add(curPosIndex, new MovementUpdate(spawnPosition));
            MovementUpdate _value;
            if(_moveUpdate.TryGetValue(curPosIndex, out _value)) 
                _value.Received();
            else { throw new Exception("Set Stats Exception: There is no value in dictionary."); }
        }
        curPosition = spawnPosition;
        moving = true;
    }

    #endregion

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            int index = curPosIndex + 1;
            Vector3 newPosition;
            Vector3 direction = new Vector3(h, 0, v);
            Ray _ray = new Ray(transform.position, direction.normalized);
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit))
            {
                newPosition = _hit.point;
            }
            else
            {
                newPosition = curPosition + direction.normalized * _playerStats.playerMoveSpeed * (float)GSC.timerTick / 1000f;
            }      
            _moveUpdate.Add(index, new MovementUpdate(newPosition));
            curPosIndex++;
            _currentLerpTime = 0f;
            RoomUDPSendData.SendMovePosition(index, newPosition);
            MovementUpdate value;
            if (_moveUpdate.TryGetValue(index, out value))
                value.Sended();
            else
                throw new Exception("Move send exception");
        }
    }

    public void CheckPosition(int index, float[] pos)
    {
        if (isPlayer)
        {
            MovementUpdate value;
            if (_moveUpdate.TryGetValue(index, out value))
            {
                if (!value.received)
                {
                    if (!value.position.Equals(new Vector3(pos[0], 0.5f, pos[1])))
                    {
                        Debug.Log(index + " pos in client: " + value.position + ", pos in server: " + pos[0] + ",0.5," + pos[1]);
                        BattleLogic.StopTimer();
                        List<int> removeIndexes = new List<int>();
                        foreach (var k in _moveUpdate.Reverse())
                        {
                            if (k.Value.received && k.Key < index)
                            {
                                curPosIndex = k.Key;
                                _currentLerpTime = 0f;
                                RoomUDPSendData.SendMoveBack(k.Key);
                                break;
                            }
                            else
                            {
                                removeIndexes.Add(k.Key);
                            }
                        }
                        foreach (int i in removeIndexes)
                        {
                            _moveUpdate.Remove(i);
                        }
                        BattleLogic.StartTimer();
                    }
                    else
                    {
                        _keys = _moveUpdate.Keys.ToArray();
                        foreach (int key in _keys)
                        {
                            if (key < index)
                                _moveUpdate.Remove(key);
                        }
                        value.Received();
                    }
                }
            }
            else
            {
                MovementUpdate _value;
                if (_moveUpdate.TryGetValue(index, out _value))
                {
                    throw new Exception("Не чекнул позицию на индексе: " + index + " и на позиции: " + pos[0] + "," + 0.5f + "," + pos[1] + ", на момент выдрасывания ошибки этот индекс присутствует и хранит позицию: " + _value.position.x + "," +_value.position.y + "," + _value.position.z + " этотскрипт висит на игроке: " + isPlayer);
                } 
                else
                {
                    throw new Exception("Не чекнул позицию на индексе: " + index + " и на позиции: " + pos[0] + "," + 0.5f + "," + pos[1] +", в словаре данный индекс отсутствует" + " этотскрипт висит на игроке: " + isPlayer);
                }
            }
        }
        else
        {
            curPosition.x = pos[0];
            curPosition.z = pos[1];
            _currentLerpTime = 0f;
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
