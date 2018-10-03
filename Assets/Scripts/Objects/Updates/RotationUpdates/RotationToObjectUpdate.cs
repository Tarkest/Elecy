﻿using UnityEngine;

public class RotationToObjectUpdate : RotationUpdate
{
    protected GameObject target;

    private void Update()
    {
        if(mObject.moving)
            mObject.transform.LookAt(target.transform);
    }

    public void Init(Quaternion value, BaseObject o, GameObject target)
    {
        base.Init(value, o);
        this.target = target;
    }

}
