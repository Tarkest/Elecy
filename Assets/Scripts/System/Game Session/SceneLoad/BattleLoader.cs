using System.Collections;
using System.Collections.Generic;
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
    }

    private void Update()
    {
        if (loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
            //RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
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

    //Переписать Нахуй
    public static void SpanwPlayers(string nickname1, string nickname2)
    {
        if (nickname1 == NetPlayerTCP.GetNickname())
        {
            RoomSendData.SendPlayerSpawned(GlobalObjects.firstSPpos, GlobalObjects.firstSProt);
            GlobalObjects.firstSpawnPoint.SpawnPlayer();
            GlobalObjects.secondSpawnPoint.SpawnDummy();
            ThisPlayerProgressChange(0.33f);
        }
        else
        {
            RoomSendData.SendPlayerSpawned(GlobalObjects.secondSPpos, GlobalObjects.secondSProt);
            GlobalObjects.firstSpawnPoint.SpawnDummy();
            GlobalObjects.secondSpawnPoint.SpawnPlayer();
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
            NetworkGameObject NewRockNet = NewRock.AddComponent<NetworkGameObject>();
            NewRockNet.SetIndex(indexes[i]);
            Vector3 pos = new Vector3(Position[i][0], Position[i][1], Position[i][2]);
            Quaternion rot = new Quaternion(Rotation[i][0], Rotation[i][1], Rotation[i][2], Rotation[i][3]);
            NewRockNet.SetTransform(pos, rot);
            RoomTCP.gameObjects.Add(NewRockNet);
            Instantiate(NewRock, pos, rot);
        }
        RoomSendData.SendRocksSpawned();
        ThisPlayerProgressChange(0.66f);
    }

    private static void LoadTrees()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject NewTree = Resources.Load("BattleArena/Tree") as GameObject;
            NetworkGameObject NewTreeNet = NewTree.AddComponent<NetworkGameObject>();
            NewTreeNet.SetIndex(indexes[i]);
            Vector3 pos = new Vector3(Position[i][0], Position[i][1], Position[i][2]);
            Quaternion rot = new Quaternion(Rotation[i][0], Rotation[i][1], Rotation[i][2], Rotation[i][3]);
            NewTreeNet.SetTransform(pos, rot);
            RoomTCP.gameObjects.Add(NewTreeNet);
            Instantiate(NewTree, pos, rot);
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
        loaded = true;
        BattleLogic.BeginBattle();
    }
}
