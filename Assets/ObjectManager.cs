using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public Camera mainCamera;

    public Vector3 mousePosition;

    public static GameObject Player;

    public static GameObject EnemyPlayer;
    

	void Awake ()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

    private void Update()
    {
        mousePosition = MouseController.mousePosition;
    }

    public static void PlayersLoaded(GameObject player, GameObject enemy)
    {
        Player = player;
        EnemyPlayer = enemy;
        RoomSendData.SendPlayerSpawned();
    }
}
