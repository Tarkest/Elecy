using UnityEngine;

public class Spell : BaseObject
{

    internal void SetStartProperties(Vector3 castPosition, Vector3 targetPosition, int index, bool isMain = false)
    {
        this.index = index;
        SetBaseStats();
        SetMovement(isMain, castPosition, targetPosition);
    }

    protected internal override void CheckPosition(int index, float[] pos)
    {
        Movement.CheckPosition(index, pos);
    }

    protected internal override void Move()
    {
        Movement.Move();
    }

    protected internal override void SetBaseStats()
    {
        Stats.SetBaseStats(this);
    }

    protected internal override void SetMovement(bool isPlayer = false, params Vector3[] pos)
    {
        Movement = GetComponent<BaseMovement>();
        Movement.SetMovement(this, isPlayer, pos);
    }

    public void NetworkDestoy()
    {
        SendDataTCP.SendDestroy(index);
    }
}

