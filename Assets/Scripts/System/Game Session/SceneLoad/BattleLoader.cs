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

    private void Awake()
    {
        _loadScreen = GameObject.Find("LoadScreen");
        _yourLoadProgress = _loadScreen.transform.Find("ThisPlayerLoad").GetComponent<Slider>();
        _enemyLoadProgress = _loadScreen.transform.Find("EnemyPlayerLoad").GetComponent<Slider>();
    }

    private void Update()
    { 
        if(loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
            //RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
        }
        _yourLoadProgress.value = thisPlayerProgress;
        _enemyLoadProgress.value = enemyPlayerProgress;

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
