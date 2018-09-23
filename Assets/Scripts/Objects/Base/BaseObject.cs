using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public PositionUpdate positionUpdate;
    public BaseStatsMenu mStats;
    public int index;

    public bool isMain;
    public bool moving;

    protected bool initiaziled;

    #region Initialization

    protected void Init(int index)
    {
        this.index = index;
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

