﻿using UnityEngine;

public abstract class BaseObject : MonoBehaviour 
{
    public BaseMovement Movement;
    public BaseStats Stats;
    public int index;

    protected internal abstract void SetMovement(bool isPlayer = false, params Vector3[] pos);
    protected internal abstract void SetBaseStats();

    #region Moving
    protected internal abstract void Move();
    protected internal abstract void CheckPosition(int index, float[] pos);
    #endregion

    public void Destroy()
    {
        Destroy(gameObject);
    }

}

