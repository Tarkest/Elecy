using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour {

    public static Camera mainCamera;

    public static Vector3 mousePosition;

    public static int objectCount = 0;

    public static GameObject[] loadedSpells = new GameObject[20];

    public static StaticProp[] staticProps;

    public static DynamicProp[] activeProps;

    #region Player
    public static GameObject Player;
    public static Vector3 playerPos;
    public static Quaternion playerRot;
    public static Vector3 playerStartPosition;
    public static Quaternion playerStartRotation;
    public static PlayerStats playerStats;
    #endregion

    #region Enemy
    public static GameObject EnemyPlayer;
    public static Vector3 enemyPos;
    public static Quaternion enemyRot;
    public static Vector3 enemyStartPosition;
    public static Quaternion enemyStartRotation;
    public static EnemyMovement enemyMovementComponent;
    #endregion

    void Awake ()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        staticProps = new StaticProp[30];
        activeProps = new DynamicProp[30];
	}

    void Update()
    {
        mousePosition = MouseController.mousePosition;
        if (Player != null)
        {
            playerPos = Player.transform.position;
            playerRot = Player.transform.rotation;
        }
        if (EnemyPlayer != null)
        {
            enemyPos = EnemyPlayer.transform.position;
            enemyRot = EnemyPlayer.transform.rotation;
        }
    }

    public static void PlayersLoaded(GameObject player, GameObject enemy)
    {
        Player = player;
        playerStats = Player.GetComponent<PlayerStats>();
        EnemyPlayer = enemy;
        enemyMovementComponent = EnemyPlayer.GetComponent<EnemyMovement>();
        RoomSendData.SendPlayerSpawned();
    }

    public static void SetStartTransform(float[] pos1, float[] pos2, float[] rot1, float[] rot2)
    {
        playerStartPosition = new Vector3(pos1[0], pos1[1], pos1[2]);
        enemyStartPosition = new Vector3(pos2[0], pos2[1], pos2[2]);
        playerStartRotation = new Quaternion(rot1[0], rot1[1], rot1[2], rot1[3]);
        enemyStartRotation = new Quaternion(rot2[0], rot2[1], rot2[2], rot2[3]);
    }

    public static void SendPlayerUpdate()
    {
        RoomSendData.SendPlayerUpdate(playerPos, playerRot, playerStats.playerCurrentHP, playerStats.playerCurrentSN, playerStats.GetPlayerEffects());
    }

    public static void SendDynamicObjectUpdate()
    {
        foreach(DynamicProp obj in activeProps)
        {
            if(obj.GetState())
            {
                obj.SendInfo();
            }
        }
    }

    public static void SendStaticObjectUpdate()
    {
        foreach(StaticProp obj in staticProps)
        {
            if (obj.CheckChange())
            {
                obj.SendInfo();
            }
        }
    }

    public static void InstantiateOnBattleField(int PlayerIndex, int ObjectIndex, int NetObjectindex, float[] Position, float[] Rotation)
    {
        Vector3 position = new Vector3(Position[0], Position[1], Position[2]);
        Quaternion rotation = new Quaternion(Rotation[0], Rotation[1], Rotation[2], Rotation[3]);
        GameObject Instance = Instantiate(loadedSpells[ObjectIndex], position, rotation);
        if(NetObjectindex >= activeProps.Length)
        {
            int Length = activeProps.Length;
            Array.Resize(ref activeProps, Length + 10);
        }
        if(PlayerIndex == RoomTCP.GetPlayerIndex())
        {
            Instance.GetComponent<ObjectNetworkListener>().RemoveComponent();
            DynamicProp prop = Instance.GetComponent<DynamicProp>();
            prop.SetIndex(NetObjectindex);
            prop.SetState(true);
        }
        else
        {
            Behavior on another computer
        }
    }

    public static void HoldUpdate(int NetObjectIndex, float[] Position, float[] Rotation)
    {
        Take update from Server
    }
}
