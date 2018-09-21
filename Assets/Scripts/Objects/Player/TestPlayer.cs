using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Player, ITest<Player>
{

    #region Variables

    public new BaseMovement Movement { get { return mObject.Movement; } }
    public new PlayerStats Stats { get { return mObject.Stats; } }
    public new BaseInvoker PlayerInvoker { get { return mObject.PlayerInvoker; } }
    public new string nickname { get { return mObject.nickname; } }
    public new int index { get { return mObject.index; } }

    public bool Player;

    public Player mObject { get; private set; }
    public Player Dummy { get; private set; }

    private SkinnedMeshRenderer playerVisibility;
    private SkinnedMeshRenderer dummyVisibility;

    #endregion

    #region Unity's

    private void Update()
    {
        if(playerVisibility != null && dummyVisibility != null)
        {
            if(Player)
            {
                playerVisibility.enabled = true;
                dummyVisibility.enabled = false;
            }
            else
            {
                dummyVisibility.enabled = true;
                playerVisibility.enabled = false;
            }
        }
    }

    #endregion

    #region Initialization

    public override void Init(int ID, string nickname, Vector3 pos, Quaternion rot, bool isPlayer = false)
    {
        SetProtected();
        mObject.Init(ID, nickname, pos, rot, isPlayer);
        Dummy.Init(ID, nickname, pos, rot);
        Player = true;
    }

    #endregion

    #region Public Commands

    public override void LoadCombinations(GameObject[] spells)
    {
        mObject.LoadCombinations(spells);
    }

    public override Vector3 GetPosition()
    {
        return mObject.GetPosition();
    }

    #endregion

    #region Overrided Test Commands

    public override void Callback()
    {
        mObject.Callback();
    }

    public override void CheckPosition(int index, float[] pos)
    {
        mObject.CheckPosition(index, pos);
        Dummy.CheckPosition(index, pos);
    }

    public override void HPOuterChange(int change)
    {
        mObject.HPOuterChange(change);
        Dummy.HPOuterChange(change);
    }

    #endregion

    #region Private Helpers

    protected void SetProtected()
    {
        GameObject _enemy = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        Dummy = _enemy.GetComponent<Player>();
        Destroy(_enemy.GetComponent<SpellInvokerIgnis>());
        _enemy.GetComponent<BoxCollider>().enabled = false;
        dummyVisibility = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        _enemy.tag = Tags.Player.ToString();
        GameObject _player = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        mObject = _player.GetComponent<Player>();
        playerVisibility = _player.GetComponentInChildren<SkinnedMeshRenderer>();
        ObjectManager.cameraTarger.player = _player.transform;
        _player.tag = Tags.Player.ToString();
    }

    #endregion

}
