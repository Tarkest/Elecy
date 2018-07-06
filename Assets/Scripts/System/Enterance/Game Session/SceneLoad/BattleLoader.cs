﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour
{
    private static GameObject _loadScreen;
    private static Slider _yourLoadProgress;
    private static Slider _enemyLoadProgress;
    private static string nickname;
    private static bool loaded = false;
    private static float thisPlayerProgress = 0f;
    private static float enemyPlayerProgress = 0f;
    private static bool PlayerSpawn = false;
    private static bool RockSpawn = false;
    private static bool TreeSpawn = false;
    private static bool LoadingSpells = false;
    private static int count;
    private static int[] indexes;
    private static float[][] Position = new float[2][];
    private static float[][] Rotation = new float[2][];
    private static int _maxHP;
    private static int _maxSN;
    private static float _moveSpeed;
    private static float _attackSpeed;
    private static int _basicDefence;
    private static int _fireDefence;
    private static int _earthDefence;
    private static int _windDefence;
    private static int _waterDefence;

    private void Awake()
    {
        _loadScreen = GameObject.Find("LoadScreen");
        _yourLoadProgress = _loadScreen.transform.Find("ThisPlayerLoad").GetComponent<Slider>();
        _enemyLoadProgress = _loadScreen.transform.Find("EnemyPlayerLoad").GetComponent<Slider>();
    }

    private void Start()
    {
        RoomSendData.SendConnectionOk(RoomTCP.Getindex());
    }

    private void Update()
    {
        if (loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
        } else
        {
            _yourLoadProgress.value = thisPlayerProgress;
            _enemyLoadProgress.value = enemyPlayerProgress;
        }

        if(PlayerSpawn)
        {
            PlayerSpawn = false;
            GameObject Player = Instantiate(Resources.Load("Players/TestPlayer"), ObjectManager.playerStartPosition, ObjectManager.playerStartRotation) as GameObject;
            GameObject EnemyPlayer = Instantiate(Resources.Load("Players/Dummy"), ObjectManager.enemyStartPosition, ObjectManager.enemyStartRotation) as GameObject;
            Player.GetComponent<PlayerStats>().SetStats(_maxHP, _maxSN, _moveSpeed, _attackSpeed, _basicDefence, _fireDefence, _earthDefence, _windDefence, _waterDefence);
            ObjectManager.PlayersLoaded(Player, EnemyPlayer);
            ThisPlayerProgressChange(0.25f);
            DeveloperScreenController.AddInfo("Player pos: " + ObjectManager.playerStartPosition.x.ToString() + ", " + ObjectManager.playerStartPosition.y.ToString() + ", " + ObjectManager.playerStartPosition.z.ToString(), 1);
            DeveloperScreenController.AddInfo("Enemy pos: " + ObjectManager.enemyStartPosition.x.ToString() + ", " + ObjectManager.enemyStartPosition.y.ToString() + ", " + ObjectManager.enemyStartPosition.z.ToString(), 1);
        }

        if(RockSpawn)
        {
            RockSpawn = false;
            LoadRocks();
        }

        if(TreeSpawn)
        {
            TreeSpawn = false;
            LoadTrees();
        }
    }

    public static void SpanwPlayers(string nickname1, string nickname2, float[][]positions, float[][]rotations)
    {
        _maxHP = 100;
        _maxSN = 1000;
        _moveSpeed = 10f;
        _attackSpeed = 10f;
        _basicDefence = 10;
        _fireDefence = 5;
        _earthDefence = 5;
        _windDefence = 5;
        _waterDefence = 5;
        if (nickname1 == NetPlayerTCP.GetNickname())
        {
            ObjectManager.SetStartTransform(positions[0], positions[1], rotations[0], rotations[1]);
            PlayerSpawn = true;
            RoomTCP.SetPlayerIndex(1);
        }
        else
        {
            ObjectManager.SetStartTransform(positions[1], positions[0], rotations[1], rotations[0]);
            PlayerSpawn = true;
            RoomTCP.SetPlayerIndex(2);
        }

    }

    public static void LoadRocks(int rocksCount, int[] index, float[][] rocksPosition, float[][] rocksRotation)
    {
        count = rocksCount;
        indexes = index;
        Position = rocksPosition;
        Rotation = rocksRotation;
        RockSpawn = true;
    }

    public static void LoadTrees(int treesCount, int[] index, float[][] treesPosition, float[][] treesRotation)
    {
        count = treesCount;
        indexes = index;
        Position = treesPosition;
        Rotation = treesRotation;
        TreeSpawn = true;
    }

    private static void LoadRocks()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject NewRock = Resources.Load("BattleArena/Rock") as GameObject;
            Vector3 pos = new Vector3(Position[i][0], Position[i][1], Position[i][2]);
            Quaternion rot = new Quaternion(Rotation[i][0], Rotation[i][1], Rotation[i][2], Rotation[i][3]);
            GameObject NewRockOnField = Instantiate(NewRock, pos, rot);
            NetworkGameObject NewRockNet = NewRockOnField.AddComponent<NetworkGameObject>();
            NewRockNet.SetIndex(indexes[i]);
            NewRockNet.SetTransform(pos, rot);
            ObjectManager.staticProps.Add(NewRockOnField);
            DeveloperScreenController.AddInfo("Rock"+i.ToString() + "\n" + " Pos: "+ pos.x.ToString()+", "+ pos.y.ToString() + ", " + pos.z.ToString() + "\n" + " Rot: " + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString(), 3);
        }
        RoomSendData.SendRocksSpawned();
        ThisPlayerProgressChange(0.5f);
    }

    private static void LoadTrees()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject NewTree = Resources.Load("BattleArena/Tree") as GameObject;
            Vector3 pos = new Vector3(Position[i][0], Position[i][1], Position[i][2]);
            Quaternion rot = new Quaternion(Rotation[i][0], Rotation[i][1], Rotation[i][2], Rotation[i][3]);
            GameObject NewTreeOnField = Instantiate(NewTree, pos, rot);
            NetworkGameObject NewTreeNet = NewTreeOnField.AddComponent<NetworkGameObject>();
            NewTreeNet.SetIndex(indexes[i]);
            NewTreeNet.SetTransform(pos, rot);
            ObjectManager.staticProps.Add(NewTreeOnField);
            DeveloperScreenController.AddInfo("Tree" + i.ToString() + "\n" + " Pos: " + pos.x.ToString() + ", " + pos.y.ToString() + ", " + pos.z.ToString() + "\n" + " Rot: " + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString(), 3);
        }
        RoomSendData.SendTreesSpawned();
        ThisPlayerProgressChange(0.75f);
    }

    public static void LoadSpells(int[] SpellsIndexes)
    {
        ObjectManager.LoadSpells(SpellsIndexes);
        DeveloperScreenController.AddInfo("Spells: ", 1);
        for(int i = 0; i < SpellsIndexes.Length; i++)
        {
            DeveloperScreenController.AddInfo(i.ToString()+ ": "+ SpellsIndexes[i].ToString(), 1);
        }
    }

    public static void EnemyProgressChange(float progress)
    {
        enemyPlayerProgress = progress;
    }

    public static void ThisPlayerProgressChange(float progress)
    {
        thisPlayerProgress = progress;
    }

    public static void StartBattle()
    {
        loaded = true;      
        BattleLogic.BeginBattle();
    }
}