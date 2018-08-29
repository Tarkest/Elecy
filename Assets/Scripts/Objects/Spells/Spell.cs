using UnityEngine;

public class Spell : BaseObject
{

    //private void Awake()
    //{
    //    SetProtected();
    //}

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
        Movement.SetMovement(this, isPlayer, pos);
    }

    //protected void SetProtected()
    //{
    //    Stats = GetComponent<SpellStats>();
    //}

    public void NetworkDestoy()
    {
        SendDataTCP.SendDestroy(index);
    }
}

