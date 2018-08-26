using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkObjectController : MonoBehaviour {

    #region Variables

    public int index;
    public int hp;
    public bool owner;

    private Rigidbody _thisRigitbody;
    public SpellProperties spell;
    public PropStats propStats;
    public Behaviour Behaviour;

    #endregion

    public void CheckPosition(int UpdateIndex, float[] pos)
    {
        Behaviour.CheckPosition(UpdateIndex, pos);
    }

    public void Move()
    {
        Behaviour.Move();
    }

    public void NetworkDestoy()
    {
        SendDataTCP.SendDestroy(index);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

public abstract class Behaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _curPosition;
    private Dictionary<int, MovementUpdate> _moveUpdate = new Dictionary<int, MovementUpdate>();
    private int _curPosIndex;
    private bool _isMain;
    private float _currentLerpTime;
    public int[] _keys;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(_isMain)
        {
            MovementUpdate _value;
            if(_moveUpdate.TryGetValue(_curPosIndex, out _value))
            {
                _curPosition = _value.position;
            }
        }
    }

    void FixedUpdate()
    {
        _currentLerpTime += Time.fixedDeltaTime;
        if (_currentLerpTime > (float)GSC.timerTick / 1000)
            _currentLerpTime = (float)GSC.timerTick / 1000;
        float _delta = _currentLerpTime * 1000 / (float)GSC.timerTick;
        _rigidbody.MovePosition(Vector3.Lerp(transform.position, _curPosition, _delta));
    }

    /// <summary>
    /// Method what get the last update what comes from the server and if indexes and positions are both the 
    /// same, then method will mark this information as last succesfull update
    /// </summary>
    /// <param name="UpdateIndex"></param>
    /// <param name="PositionToCompare"></param>
    /// <returns></returns>
    public void CheckPosition(int index, float[] pos)
    {
        if (_isMain)
        {
            lock (_moveUpdate)
            {
                MovementUpdate value;
                if (_moveUpdate.TryGetValue(index, out value))
                {
                    if (!value.received)
                    {
                        if (!value.position.Equals(new Vector3(pos[0], 0.5f, pos[1])))
                        {
                            BattleLogic.StopTimer();
                            List<int> removeIndexes = new List<int>();
                            foreach (var k in _moveUpdate.Reverse())
                            {
                                if (k.Value.received && k.Key < index)
                                {
                                    _curPosIndex = k.Key;
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
            }
        }
        else
        {
            _curPosition.x = pos[0];
            _curPosition.z = pos[1];
            _currentLerpTime = 0f;
        }
    }

    public abstract void Move();

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