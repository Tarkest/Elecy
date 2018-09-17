using UnityEngine;

public abstract class BaseObject : MonoBehaviour, ITickCallback, ICheckPosition, IHPOuterChange
{
    public BaseMovement mMovement;
    public BaseStats mStats;
    public int index;


    #region Initialization

    protected void Init(int index)
    {
        this.index = index;
    }

    #endregion

    #region Tick Callback

    public virtual void Callback()
    {
        mStats.HPUpdate();
        mMovement.Move();
    }

    #endregion

    #region Moving

    public virtual void CheckPosition(int index, float[] pos)
    {
        mMovement.CheckPosition(index, pos);
    }

    #endregion

    #region HPChange

    public virtual void HPOuterChange(int change)
    {
        mStats.HPOuterChange(change);
    }

    #endregion

    public void Destroy()
    {
        Destroy(gameObject);
    }


}

