using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour
{
    private static GameObject _loadScreen;
    private static string nickname;
    private static bool spawn = false;
    private static bool loaded = false;


    private void Awake()
    {
        _loadScreen = GameObject.Find("LoadScreen");
    }

    private void Update()
    { 
        if(loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
            //RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
        }

    }
    public static void SpanwPlayers(string nickname1, string nickname2)
    {
        if (nickname1 == NetPlayerTCP.GetNickname())
        {
            RoomSendData.SendPlayerSpawned(GlobalObjects.firstSPpos, GlobalObjects.firstSProt);
            GlobalObjects.firstSpawnPoint.SpawnPlayer();
            GlobalObjects.secondSpawnPoint.SpawnDummy();
        }
        else
        {
            RoomSendData.SendPlayerSpawned(GlobalObjects.secondSPpos, GlobalObjects.secondSProt);
            GlobalObjects.firstSpawnPoint.SpawnDummy();
            GlobalObjects.secondSpawnPoint.SpawnPlayer();
        }
    }

    public static void StartBattle()
    {
        loaded = true;
        BattleLogic.BeginBattle();
    }
}
