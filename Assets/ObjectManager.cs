using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public Camera mainCamera;

    public Vector3 mousePosition;

    public static GameObject Player;
    public static Vector3 playerPos;
    public static Quaternion playerRot;

    public static GameObject EnemyPlayer;
    public static Vector3 enemyPos;
    public static Quaternion enemyRot;

    public static Vector3 playerStartPosition;
    public static Vector3 enemyStartPosition;
    public static Quaternion playerStartRotation;
    public static Quaternion enemyStartRotation;

	void Awake ()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

    private void Update()
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
        EnemyPlayer = enemy;
        RoomSendData.SendPlayerSpawned();
    }

    public static void SetStartTransform(float[] pos1, float[] pos2, float[] rot1, float[] rot2)
    {
        playerStartPosition = new Vector3(pos1[0], pos1[1], pos1[2]);
        enemyStartPosition = new Vector3(pos2[0], pos2[1], pos2[2]);
        playerStartRotation = new Quaternion(rot1[0], rot1[1], rot1[2], rot1[3]);
        enemyStartRotation = new Quaternion(rot2[0], rot2[1], rot2[2], rot2[3]);
    }
}
