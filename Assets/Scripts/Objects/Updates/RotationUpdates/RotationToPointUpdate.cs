using UnityEngine;

public class RotationToPointUpdate : RotationUpdate
{
    protected Vector3 target;

    private void Update()
    {
        if(mObject.moving)
            mObject.transform.LookAt(target);
    }

    public void Init(Quaternion value, BaseObject o, Vector3 target)
    {
        base.Init(value, o);
        this.target = target;
    }

}