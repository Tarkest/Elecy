using UnityEngine;

public class Player : BaseObject, IStatsMenuSpecifier<PlayerMenu>
{

    #region Public Variables

    public string nickname;
    public BaseInvoker PlayerInvoker;
    public SynergyUpdate synergyUpdate;

    public PlayerMenu Stats
    {
        get
        {
            return mStats as PlayerMenu;
        }
    }

    internal Rigidbody playerRigidbody;

    #endregion

    #region Stats

    public int CurrentSynergy;

    #endregion

    #region Public Commands

    public virtual void Init(int index, string nickname, Vector3 pos, Quaternion rot, bool isMain = false)
    {
        synergyUpdate = gameObject.AddComponent<SynergyUpdate>();
        rotationUpdate = gameObject.AddComponent<RotationToObjectUpdate>();
        (rotationUpdate as RotationToObjectUpdate).Init(rot, this, MouseController.Object);
        base.Init(index, ObjectType.player);
        this.nickname = nickname;
        positionUpdate.Init(pos, this);
        hpUpdate.Init(Stats.MaxHP, this);
        synergyUpdate.Init(Stats.MaxSN, this);
        this.moving = true;
        this.isMain = isMain;
        initiaziled = true;
    }

    public virtual void LoadCombinations(GameObject[] spells)
    {
        PlayerInvoker.Init(this, spells);
    }

    public virtual Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    #endregion



}
