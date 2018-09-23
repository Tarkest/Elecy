using UnityEngine;

public class Spell : BaseObject, IPositionUpdateSpecifier<SpellPositionUpdate>, IStatsMenuSpecifier<SpellMenu>
{

    #region Public Members

    public SpellPositionUpdate PositionUpdate
    {
        get
        {
            return positionUpdate as SpellPositionUpdate;
        }
    }

    public SpellMenu Stats
    {
        get
        {
            return mStats as SpellMenu;
        }
    }

    public short variationHash;

    #endregion

    #region Stats

    public float CurrentSpeed;

    #endregion

    #region Init

    public void Init(Vector3 castPos, Vector3 targetPos, int index, bool isMain = false)
    {
        base.Init(index);
        PositionUpdate.Init(castPos, targetPos, this);
        initiaziled = true;
    }

    #endregion

    public void NetworkDestoy()
    {
        SendDataTCP.SendDestroy(index);
    }

    #region Hash

    public bool CheckHash(short hash)
    {
        if (variationHash == hash)
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

