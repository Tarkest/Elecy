using UnityEngine;

public class SpellStats : BaseStats, IStatsMenuSpecifier<SpellMenu>
{

    public short variationHash;

    public float CurrentSpeed;

    public SpellMenu Stats
    {
        get
        {
            return mStats as SpellMenu;
        }
    }

    public void Init(BaseObject obj)
    {
        base.mInit(obj);
        CurrentSpeed = Stats.Speed;
        SetSpellMovement();
    }

    #region Spell Movement

    protected void SetSpellMovement()
    {
        switch(Stats.Movement)
        {
            case SpellMovement.CasterToPointMovement:
                baseObject.mMovement = this.gameObject.AddComponent<CasterToPointMovement>();
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
