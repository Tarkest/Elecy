using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : Player
{
    public Player player;
    public Player dummy;
    public bool Player;

    protected internal PlayerMovement DummyMovement;
    protected internal PlayerStats DummyStats;
   
    private BoxCollider dummyCollider;
    private SkinnedMeshRenderer playerVisibility;
    private SkinnedMeshRenderer dummyVisibility;

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

    public override void SetStartProperties(string nickname, Vector3 pos, Quaternion rot, int ID, bool isPlayer = false)
    {
        this.nickname = nickname;
        startPosition = pos;
        startRotation = rot;
        this.ID = ID;
        player.PlayerMovement.SetStats(startPosition, isPlayer);
        dummy.PlayerMovement.SetStats(startPosition);
        Player = true;
    }

    public override void SetStats(int maxHP, int maxSN, float moveSpeed, float attackSpeed, int basicDefence, int fireDefence, int earthDefence, int windDefence, int waterDefence)
    {
        player.PlayerStats.SetStats(maxHP, maxSN, moveSpeed, attackSpeed, basicDefence, fireDefence, earthDefence, windDefence, waterDefence);
    }

    public override void Move()
    {
        player.PlayerMovement.Move();
    }

    public override void CheckPosition(int updateIndex, float[] pos)
    {
        player.PlayerMovement.CheckPosition(updateIndex, pos);
        dummy.PlayerMovement.CheckPosition(updateIndex, pos);
    }

    protected internal override void SetProtected()
    {
        GameObject _enemy = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        dummy = _enemy.GetComponent<Player>();
        dummy.PlayerMovement = _enemy.GetComponent<PlayerMovement>();
        dummy.PlayerStats = _enemy.GetComponent<PlayerStats>();
        dummyCollider = _enemy.GetComponent<BoxCollider>();
        dummyVisibility = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        dummyCollider.enabled = false;
        GameObject _player = Instantiate(Resources.Load("Players/Player"), Network.currentManager.GetStartPosition(0), Network.currentManager.GetStartRotation(0), this.transform) as GameObject;
        player = _player.GetComponent<Player>();
        player.PlayerMovement = _player.GetComponent<PlayerMovement>();
        player.PlayerStats = _player.GetComponent<PlayerStats>();
        playerVisibility = _player.GetComponentInChildren<SkinnedMeshRenderer>();
        ObjectManager.cameraTarger.player = _player.transform;
    }

}
