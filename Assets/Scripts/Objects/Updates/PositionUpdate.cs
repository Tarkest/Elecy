using UnityEngine;

public abstract class PositionUpdate : BaseUpdate<Vector3>
{

    public float currentLerpTime = 0f;

    protected override void SendStepBack()
    {
        currentLerpTime = 0f;
        SendDataUDP.SendMoveBack(currentIndex);
    }

}

public abstract class SpellPositionUpdate : PositionUpdate, IBaseObjectSpecifier<Spell>
{
    public Spell BaseObject
    {
        get
        {
            return mObject as Spell;
        }
    }
    protected Vector3 TargetPosition;
    public bool Destroying;

    public void Init(Vector3 spawnPos, Vector3 targetPos, BaseObject o)
    {
        base.Init(spawnPos, o);
        TargetPosition = targetPos;
    }
}

