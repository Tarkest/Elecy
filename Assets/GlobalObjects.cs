﻿using System.Collections;
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

     void Awake()
    { 
        player = GameObject.Find("Test player");
        playerTransform = player.GetComponent<Transform>();
        playerPos = playerTransform.position;
        playerRot = playerTransform.rotation;
        enemy = GameObject.Find("dummy");
        enemyTransform = enemy.GetComponent<Transform>();
        enemyMovement = enemy.GetComponent<EnemyMovement>();
    }

    void FixedUpdate()
    {
        playerPos = playerTransform.position;
        playerRot = playerTransform.rotation;
        enemyPos = enemyTransform.position;
        enemyRot = enemyTransform.rotation;
    }
}
