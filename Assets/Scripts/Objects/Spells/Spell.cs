using System;
using UnityEngine;

public class Spell : BaseObject, IStatsMenuSpecifier<SpellMenu>
{

    #region Public Members

    public SpellMenu Stats
    {
        get
        {
            return mStats as SpellMenu;
        }
    }

    public BaseDamage DamageComponent;

    public short variationHash;

    #endregion


    #region Init

    public void Init(Vector3 castPos, Vector3 targetPos, int index, bool isMain = false)
    {

        #region CheckPosition

        Stats.SetMovement(GetComponent<PositionUpdate>());
        if (!Stats.Movement.Equals(PositionUpdateEnum.ToPoint))
        {
            throw new Exception("Spell " + index + " has wrong position update type!");
        }

        #endregion

        #region CheckRotation

        Stats.SetRotation(GetComponent<RotationUpdate>());
        if (!Stats.Rotation.Equals(RotationUpdateEnum.ToPoint))
        {
            throw new Exception("Spell" + index + " has wrong rotation update type!");
        }

        #endregion

        base.Init(index, ObjectType.spell);
        (positionUpdate as PositionToPointUpdate).Init(castPos, targetPos, this);
        (rotationUpdate as RotationToPointUpdate).Init(Quaternion.identity, this, targetPos);
        hpUpdate.Init(Stats.MaxHP, this);
        this.moving = true;
        this.isMain = isMain;
        initiaziled = true;
    }

    public void Init(Vector3 castPos, GameObject targetObj, int index, bool isMain = false)
    {

        #region Check Position

        Stats.SetMovement(GetComponent<PositionUpdate>());
        if (!Stats.Movement.Equals(PositionUpdateEnum.ToObject))
        {
            throw new Exception("Spell " + index + " has wrong position update type!");
        }

        #endregion

        #region CheckRotation

        Stats.SetRotation(GetComponent<RotationUpdate>());
        if (!Stats.Rotation.Equals(RotationUpdateEnum.ToObject))
        {
            throw new Exception("Spell" + index + " has wrong rotation update type!");
        }

        #endregion


        base.Init(index, ObjectType.spell);
        (positionUpdate as PositionToObjectUpdate).Init(castPos, targetObj, this);
        (rotationUpdate as RotationToObjectUpdate).Init(Quaternion.identity, this, targetObj);
        hpUpdate.Init(Stats.MaxHP, this);
        initiaziled = true;
    }

    public void Init(Vector3 castPos, Vector3 targetPos, GameObject targetObj, int index, bool isMain = false)
    {

        #region CheckPosition

        Stats.SetMovement(GetComponent<PositionUpdate>());
        if (Stats.Movement.Equals(PositionUpdateEnum.ToPoint))
        {
            (positionUpdate as PositionToPointUpdate).Init(castPos, targetPos, this);
        }
        else if(Stats.Movement.Equals(PositionUpdateEnum.ToObject))
        {
            (positionUpdate as PositionToObjectUpdate).Init(castPos, targetObj, this);
        }
        else
        {
            throw new Exception("Spell " + index + " has no available position update");
        }

        #endregion

        #region CheckRotation

        Stats.SetRotation(GetComponent<RotationUpdate>());
        if (Stats.Rotation.Equals(RotationUpdateEnum.ToPoint))
        {
            (rotationUpdate as RotationToPointUpdate).Init(Quaternion.identity, this, targetPos);
        }
        else if (Stats.Rotation.Equals(RotationUpdateEnum.ToObject))
        {
            (rotationUpdate as RotationToObjectUpdate).Init(Quaternion.identity, this, targetObj);
        }
        else
        {
            throw new Exception("Spell " + index + " has no available position update");
        }

        #endregion

        base.Init(index, ObjectType.spell);
        hpUpdate.Init(Stats.MaxHP, this);
        initiaziled = true;
    }

    #endregion

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

