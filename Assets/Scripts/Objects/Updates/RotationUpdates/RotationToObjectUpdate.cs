using UnityEngine;

public class RotationToObjectUpdate : RotationUpdate
{
    protected GameObject target;

    private void Update()
    {
        mObject.transform.LookAt(target.transform);
    }

    public void Init(Quaternion value, BaseObject o, GameObject target)
    {
        base.Init(value, o);
        this.target = target;
    }

}
