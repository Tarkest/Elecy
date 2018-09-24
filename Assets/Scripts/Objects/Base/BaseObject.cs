using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public PositionUpdate positionUpdate;
    public HPUpdate hpUpdate;
    public BaseStatsMenu mStats;
    public int index;
    public ObjectType type;
    public bool isMain;
    public bool moving;
    internal Rigidbody mRigidbody;
    protected bool initiaziled;

    #region Initialization

    protected void Init(int index, ObjectType type)
    {
        this.index = index;
        this.type = type;
        hpUpdate = new HPUpdate();
        mRigidbody = transform.GetComponent<Rigidbody>();
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

    public void Destroy()
    {
        Destroy(gameObject);
    }


}

