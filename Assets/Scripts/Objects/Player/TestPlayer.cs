using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : BaseObject, IPlayer
{

    #region Variables

    public Player player;
    public Player dummy;
    internal new PlayerMovement Movement { get { return player.Movement as PlayerMovement; } }
    internal new PlayerStats Stats { get { return player.Stats as PlayerStats; } }
    internal SpellInvokerIgnis PlayerInvoker { get { return player.PlayerInvoker; } }
    public bool Player;
    internal string nickname { get { return player.nickname; } }
    internal Vector3 startPosition { get { return player.startPosition; } }
    internal Quaternion startRotation { get { return player.startRotation; } }
    internal new int index { get { return player.index; } }

    private SkinnedMeshRenderer playerVisibility;
    private SkinnedMeshRenderer dummyVisibility;

    #endregion

    #region Unity's

    private void Awake()
    {
        SetProtected();
    }

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

    public void SetStartProperties(string nickname, Vector3 pos, Quaternion rot, int ID, bool isPlayer = false)
    {
        player.SetStartProperties(nickname, pos, rot, ID, isPlayer);
        dummy.SetStartProperties(nickname, pos, rot, ID);
        Player = true;
    }

    #endregion

    #region Public Commands

    protected internal override void Invoke()
    {
        player.Invoke();
    }

    protected internal override void CheckPosition(int updateIndex, float[] pos)
    {
        player.Movement.CheckPosition(updateIndex, pos);
        dummy.Movement.CheckPosition(updateIndex, pos);
    }

    public void LoadCombinations(List<GameObject> spells)
    {
        player.LoadCombinations(spells);
    }

    public Vector3 GetPosition()
    {
        return player.GetPosition();
    }

    #endregion

    #region Private Helpers

    protected void SetProtected()
    {
        GameObject _enemy = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        dummy = _enemy.GetComponent<Player>();
        Destroy(_enemy.GetComponent<SpellInvokerIgnis>());
        _enemy.GetComponent<BoxCollider>().enabled = false;
        dummyVisibility = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        _enemy.tag = Tags.Player.ToString();
        GameObject _player = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        player = _player.GetComponent<Player>();
        playerVisibility = _player.GetComponentInChildren<SkinnedMeshRenderer>();
        ObjectManager.cameraTarger.player = _player.transform;
        _player.tag = Tags.Player.ToString();
    }

    #endregion

    #region Not Implemented (no use)

    protected internal override void SetMovement(bool isPlayer = false, params Vector3[] pos) { }

    protected internal override void SetBaseStats() { }

    #endregion

}
