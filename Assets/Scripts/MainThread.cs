using System;
using System.Collections.Generic;
using UnityEngine;


public class MainThread : MonoBehaviour
{

    #region Variables

    private static List<Action> actionQueuesUpdateFunc = new List<Action>();
    List<Action> actionCopiedQueueUpdateFunc = new List<Action>();
    private volatile static bool noActionQueueToExecuteUpdateFunc = true;

    private static List<Action> actionQueuesFixedUpdateFunc = new List<Action>();
    List<Action> actionCopiedQueueFixedUpdateFunc = new List<Action>();
    private volatile static bool noActionQueueToExecuteFixedUpdateFunc = true;

    #endregion

    #region Update

    public static void executeInUpdate(Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action");
        }

        lock (actionQueuesUpdateFunc)
        {
            actionQueuesUpdateFunc.Add(action);
            noActionQueueToExecuteUpdateFunc = false;
        }
    }

    public void Update()
    {
        if (noActionQueueToExecuteUpdateFunc)
        {
            return;
        }

        actionCopiedQueueUpdateFunc.Clear();
        lock (actionQueuesUpdateFunc)
        {
            actionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc);
            actionQueuesUpdateFunc.Clear();
            noActionQueueToExecuteUpdateFunc = true;
        }

        for (int i = 0; i < actionCopiedQueueUpdateFunc.Count; i++)
        {
            actionCopiedQueueUpdateFunc[i].Invoke();
        }
    }

    #endregion

    #region FixedUpdate

    public static void executeInFixedUpdate(Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action");
        }

        lock (actionQueuesFixedUpdateFunc)
        {
            actionQueuesFixedUpdateFunc.Add(action);
            noActionQueueToExecuteFixedUpdateFunc = false;
        }
    }

    public void FixedUpdate()
    {
        if (noActionQueueToExecuteFixedUpdateFunc)
        {
            return;
        }

        actionCopiedQueueFixedUpdateFunc.Clear();
        lock (actionQueuesFixedUpdateFunc)
        {
            actionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc);
            actionQueuesFixedUpdateFunc.Clear();
            noActionQueueToExecuteFixedUpdateFunc = true;
        }

        for (int i = 0; i < actionCopiedQueueFixedUpdateFunc.Count; i++)
        {
            actionCopiedQueueFixedUpdateFunc[i].Invoke();
        }
    }

    #endregion

}

