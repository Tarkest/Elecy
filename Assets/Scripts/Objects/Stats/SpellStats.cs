using UnityEngine;

public class SpellStats : BaseStats
{

    public short variationHash;

    public int CurrentHP;
    public float CurrentSpeed;

    internal override void SetBaseStats(BaseObject obj)
    {
        base.SetBaseStats(obj);
        CurrentHP = (stats as SpellMenu).SpellMaxHP;
        CurrentSpeed = (stats as SpellMenu).Speed;
        SetSpellMovement();
    }

    #region Spell Movement

    protected void SetSpellMovement()
    {
        switch((stats as SpellMenu).Movement)
        {
            case SpellMovement.CasterToPointMovement:
                this.gameObject.AddComponent<CasterToPointMovement>();
                break;
        }
    }

    #endregion

    #region Hash

    public bool CheckHash(short hash)
    {
        if(variationHash == hash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject GetPrefab()
    {
        return gameObject;
    }

    #endregion

}
