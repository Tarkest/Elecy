﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerOld : MonoBehaviour {

    public static Camera mainCamera;

    public static Vector3 mousePosition;

    public static int objectCount = 0;

    public static GameObject[] loadedSpells;

    public static List<GameObject> staticProps;

    public static List<GameObject> activeProps;

    public static int[] Spells;

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
        staticProps = new List<GameObject>();
        activeProps = new List<GameObject>();
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
        //SendDataTCP.SendPlayerSpawned();
    }

    public static void LoadSpells(int[] spellsNumbers)
    {
        loadedSpells = new GameObject[spellsNumbers.Length];
        foreach(int i in spellsNumbers)
        {
            //loadedSpells[Array.IndexOf(spellsNumbers, i)] = Resources.Load("Spells/" + spellsNumbers[Array.IndexOf(spellsNumbers, i)]) as GameObject; 
        }
        Spells = spellsNumbers;
        //SendDataTCP.SendLoadComplite();
    }

    public static void SetStartTransform(float[] pos1, float[] pos2, float[] rot1, float[] rot2)
    {
        playerStartPosition = new Vector3(pos1[0], pos1[1], pos1[2]);
        enemyStartPosition = new Vector3(pos2[0], pos2[1], pos2[2]);
        playerStartRotation = new Quaternion(rot1[0], rot1[1], rot1[2], rot1[3]);
        enemyStartRotation = new Quaternion(rot2[0], rot2[1], rot2[2], rot2[3]);
    }
}