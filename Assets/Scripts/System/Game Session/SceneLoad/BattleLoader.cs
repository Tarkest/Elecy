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
    private static bool spawn = false;
    private static bool loaded = false;
    private static float thisPlayerProgress = 0f;
    private static float enemyPlayerProgress = 0f;
    private static Timer loadTimer;

    private static bool RockSpawn = false;
    private static bool TreeSpawn = false;
    private static int count;
    private static int[] indexes;
    private static float[][] Position;
    private static float[][] Rotation;


    private void Awake()
    {
        _loadScreen = GameObject.Find("LoadScreen");
        _yourLoadProgress = _loadScreen.transform.Find("ThisPlayerLoad").GetComponent<Slider>();
        _enemyLoadProgress = _loadScreen.transform.Find("EnemyPlayerLoad").GetComponent<Slider>();
        loadTimer = new Timer(LoadSend, null, 0, 1000 / 2);
    }

    private void Update()
    {
        if (loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
        }
        _yourLoadProgress.value = thisPlayerProgress;
        _enemyLoadProgress.value = enemyPlayerProgress;

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

    private void LoadSend(object o)
    {
        RoomSendData.SendLoadProgress(RoomTCP.Getindex(), thisPlayerProgress);
    }

    //Переписать Нахуй
    public static void SpanwPlayers(string nickname1, string nickname2)
    {
        if (nickname1 == NetPlayerTCP.GetNickname())
        {
            GlobalObjects.firstSpawnPoint.SpawnPlayer();
            GlobalObjects.secondSpawnPoint.SpawnDummy();
            RoomSendData.SendPlayerSpawned();
            ThisPlayerProgressChange(0.33f);
        }
        else
        {
            GlobalObjects.firstSpawnPoint.SpawnDummy();
            GlobalObjects.secondSpawnPoint.SpawnPlayer();
            RoomSendData.SendPlayerSpawned();
            ThisPlayerProgressChange(0.33f);
        }

    }

    public static void LoadRocks(int rocksCount, int[] index, float[][] rocksPosition, float[][] rocksRotation)
    {
        count = rocksCount;
        indexes = index;
        Position = rocksPosition;
        Rotation = rocksRotation;
        RockSpawn = true;
        RoomTCP.objectCount += count;
    }

    public static void LoadTrees(int treesCount, int[] index, float[][] treesPosition, float[][] treesRotation)
    {
        count = treesCount;
        indexes = index;
        Position = treesPosition;
        Rotation = treesRotation;
        TreeSpawn = true;
        RoomTCP.objectCount += count;
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
            RoomTCP.gameObjects.Add(NewRockNet);
        }
        RoomSendData.SendRocksSpawned();
        ThisPlayerProgressChange(0.66f);
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
            RoomTCP.gameObjects.Add(NewTreeNet);          
        }
        RoomSendData.SendLoadComplite();
        ThisPlayerProgressChange(1f);
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
        loadTimer.Dispose();
        loaded = true;      
        BattleLogic.BeginBattle();
    }
}
