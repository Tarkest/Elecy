using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseUpdate<T> : MonoBehaviour
{

    #region Protected Properties

    protected Dictionary<int, UpdateContainer<T>> updateLibrary;
    protected object locker;
    protected BaseObject mObject;

    #endregion

    #region Public Properties

    public int currentIndex;
    public T currentValue;

    #endregion

    #region Constructor

    public void Init(T value, BaseObject o)
    {
        updateLibrary = new Dictionary<int, UpdateContainer<T>>();
        mObject = o;
        locker = new object();
        currentValue = value;
        currentIndex = 1;
        updateLibrary.Add(currentIndex, new UpdateContainer<T>(currentValue));
    }

    #endregion

    #region Public Commands

    public virtual void Handle(int index, T value)
    {
        if(mObject.isMain)
        {
            lock (locker)
            {
                UpdateContainer<T> _value;
                if (updateLibrary.TryGetValue(index, out _value))
                {
                    if (!_value.received)
                    {
                        if (!_value.value.Equals(value))
                        {
                            BattleLogic.StopTimer();
                            List<int> removeIndexes = new List<int>();
                            foreach (var k in updateLibrary.Reverse())
                            {
                                if (k.Value.received && k.Key < index)
                                {
                                    currentIndex = k.Key;
                                    currentValue = k.Value.value;
                                    SendStepBack();
                                    break;
                                }
                                else
                                {
                                    removeIndexes.Add(k.Key);
                                }
                            }
                            foreach (int i in removeIndexes)
                            {
                                updateLibrary.Remove(i);
                            }
                            BattleLogic.StartTimer();
                        }
                        else
                        {
                            int[] _keys = updateLibrary.Keys.ToArray();
                            foreach (int key in _keys)
                            {
                                if (key < index)
                                    updateLibrary.Remove(key);
                            }
                            _value.Received();
                        }
                    }
                }
            }
        }
        else
        {
            currentValue = value;
        }

    }

    #endregion

    #region Abstract Commands

    public abstract void Callback();

    protected abstract void SendStepBack();

    #endregion

}

public struct UpdateContainer<T>
{
    public readonly T value;
    public bool sent { get; private set; }
    public bool received { get; private set; }

    public UpdateContainer(T value)
    {
        this.value = value;
        sent = false;
        received = false;
    }

    public void Sent()
    {
        sent = true;
    }

    public void Received()
    {
        received = true;
    }
}


