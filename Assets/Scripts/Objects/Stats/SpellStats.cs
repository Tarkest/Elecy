using UnityEngine;

public class SpellStats : BaseStats
{

    public short variationHash;

    public int CurrentHP;
    public float CurrentSpeed;

    internal override void SetBaseStats(BaseObject obj)
    {
        base.SetBaseStats(obj);
        CurrentHP = (stats as SpellMenu).spellMaxHP;
        CurrentSpeed = (stats as SpellMenu).speed;
    }

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
