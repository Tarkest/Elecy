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
}
