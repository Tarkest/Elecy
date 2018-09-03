using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject, IPlayer
{

    #region Variables

    public SpellInvokerIgnis PlayerInvoker;
    public string nickname;
    public Vector3 startPosition;
    public Quaternion startRotation;

    #endregion

    #region Public Commands

    public void SetStartProperties(string nickname, Vector3 pos, Quaternion rot, int ID, bool isPlayer = false)
    {
        this.nickname = nickname;
        startPosition = pos;
        startRotation = rot;
        index = ID;
        SetMovement(isPlayer, pos);
        SetBaseStats();
    }

    public virtual void LoadCombinations(List<GameObject> spells)
    {
        PlayerInvoker.LoadCombinations(spells);
    }

    public virtual Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    protected internal override void Invoke()
    {
        Movement.Move();
    }

    protected internal override void CheckPosition(int updateIndex, float[] pos)
    {
        Movement.CheckPosition(updateIndex, pos);
    }

    #endregion

    #region Private Helpers

    protected internal override void SetMovement(bool isPlayer = false, params Vector3[] pos)
    {
        Movement.SetMovement(this, isPlayer, pos[0]);
    }

    protected internal override void SetBaseStats()
    {
        Stats.SetBaseStats(this);
    }

    #endregion

}
