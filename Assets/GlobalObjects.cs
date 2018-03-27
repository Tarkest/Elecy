using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjects : MonoBehaviour {

    public static GameObject player;
    public static Transform playerTransform;
    public static Vector3 playerPos;
    public static Quaternion playerRot;

    public static GameObject enemy;
    public static Transform enemyTransform;
    public static EnemyMovement enemyMovement;
    public static Vector3 enemyPos;
    public static Quaternion enemyRot;

    public static GameObject loadScreen;
    public static Vector3 firstSPpos;
    public static Vector3 secondSPpos;
    public static Quaternion firstSProt;
    public static Quaternion secondSProt;

    public static Transform terrain;
    public static float terrain_x;
    public static float terrain_z;

    void Awake()
    { 
        player = GameObject.Find("Test player");
        playerTransform = player.GetComponent<Transform>();
        playerPos = playerTransform.position;
        Debug.Log(playerPos);
        playerRot = playerTransform.rotation;
        enemy = GameObject.Find("dummy");
        enemyTransform = enemy.GetComponent<Transform>();
        enemyMovement = enemy.GetComponent<EnemyMovement>();
        terrain = GameObject.Find("Terrain").GetComponent<Transform>();
        terrain_x = terrain.localScale.x;
        terrain_z = terrain.localScale.z;
    }

    void FixedUpdate()
    {
        playerPos = playerTransform.position;
        playerRot = playerTransform.rotation;
        enemyPos = enemyTransform.position;
        enemyRot = enemyTransform.rotation;
    }
}
