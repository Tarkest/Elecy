using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected internal PlayerMovement PlayerMovement;
    protected internal PlayerStats PlayerStats;

    public string nickname;
    public Vector3 startPosition;
    public Quaternion startRotation;
    public int ID;

    #region Unity's

    private void Awake()
    {
        SetProtected();
    }

    #endregion

    #region Public Commands

    public virtual void SetStartProperties(string nickname, Vector3 pos, Quaternion rot, int ID, bool isPlayer = false)
    {
        this.nickname = nickname;
        startPosition = pos;
        startRotation = rot;
        this.ID = ID;
        PlayerMovement.SetStats(startPosition, isPlayer);
    }

    #region Movement

    public virtual void Move()
    {
        PlayerMovement.Move();
    }

    public virtual void CheckPosition(int updateIndex, float[] pos)
    {
        PlayerMovement.CheckPosition(updateIndex, pos);
    }

    #endregion

    #region Stats

    public virtual void SetStats(int maxHP, int maxSN, float moveSpeed, float attackSpeed, int basicDefence, int fireDefence, int earthDefence, int windDefence, int waterDefence)
    {
        PlayerStats.SetStats(1000, 1000, 10f, 10f, 10, 5, 5, 5, 5);
    }

    #endregion

    #endregion

    #region Private Helpers

    protected internal virtual void SetProtected()
    {
        PlayerMovement = this.transform.GetComponent<PlayerMovement>();
        PlayerStats = this.transform.GetComponent<PlayerStats>();
    }

    #endregion

}
