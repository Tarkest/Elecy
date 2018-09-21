using UnityEngine;

public abstract class BaseObject : MonoBehaviour, ITickCallback, ICheckPosition, IHPOuterChange
{
    public BaseMovement mMovement;
    public BaseStats mStats;
    public int index;
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
            mStats.HPUpdate();
            mMovement.Move();
        }
    }

    #endregion

    #region Moving

    public virtual void CheckPosition(int index, float[] pos)
    {
        if(initiaziled)
            mMovement.CheckPosition(index, pos);
    }

    #endregion

    #region HPChange

    public virtual void HPOuterChange(int change)
    {
        if(initiaziled)
            mStats.HPOuterChange(change);
    }

    #endregion

    public void Destroy()
    {
        Destroy(gameObject);
    }


}

