using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public PositionUpdate positionUpdate;
    public RotationUpdate rotationUpdate;
    public HPUpdate hpUpdate;
    public BaseStatsMenu mStats;
    public int index;
    public ObjectType type;
    public bool isMain;
    public bool moving;
    public bool isAlly;
    protected bool initiaziled;
    public bool Destroying;

    #region Stats

    public float CurrentMoveSpeed;

    #endregion

    #region Initialization

    protected void Init(int index, ObjectType type, bool? isAlly = null)
    {
        this.index = index;
        this.type = type;
        if (isAlly != null)
            this.isAlly = (bool)isAlly;
        hpUpdate = gameObject.AddComponent<HPUpdate>();
        SetStartStats();
    }

    #endregion

    #region Tick Callback

    public virtual void Callback()
    {
        if(initiaziled)
        {
            positionUpdate.Callback();
        }
    }

    #endregion

    public virtual void Destroy(bool destroy = false)
    {
        if (destroy)
            Destroy(gameObject);
        else
        {
            Destroying = true;
            SendDataTCP.SendDestroy(type, index);
        }

    }

    public abstract void GetDamage(int PhysicDamage, int IgnisDamage, int TerraDamage, int CaeliDamage, int AquaDamage, int PureDamage, bool Heal);

    #region Private Helpers

    protected void SetStartStats()
    {
        CurrentMoveSpeed = mStats.BaseMoveSpeed;
    }

    #endregion

}

