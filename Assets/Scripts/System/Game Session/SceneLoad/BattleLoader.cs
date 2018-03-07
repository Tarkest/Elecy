using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour {

    private static SpawnPoint _firstSpawnPoint;
    private static SpawnPoint _secondSpawnPoint;
    private static GameObject _loadScreen;
    private static string nickname;
    private static bool spawn = false;
    private static bool loaded = false;


    private void Awake()
    {
        _firstSpawnPoint = gameObject.transform.Find("SpawnPoint1").GetComponent<SpawnPoint>();
        _secondSpawnPoint = gameObject.transform.Find("SpawnPoint2").GetComponent<SpawnPoint>();
        _loadScreen = GameObject.Find("LoadScreen");
    }

    private void Update()
    {
        if(spawn)
        {
            if (nickname == NetPlayerTCP.GetNickname())
            {
                spawn = false;
                RoomSendData.SendLoadComplite(_firstSpawnPoint.transform.position, _firstSpawnPoint.transform.rotation);
                _firstSpawnPoint.SpawnPlayer();
                _secondSpawnPoint.SpawnDummy();
            }
            else
            {
                spawn = false;
                RoomSendData.SendLoadComplite(_secondSpawnPoint.transform.position, _secondSpawnPoint.transform.rotation);
                _firstSpawnPoint.SpawnDummy();
                _secondSpawnPoint.SpawnPlayer();
            }


        }

        if(loaded)
        {
            loaded = false;
            _loadScreen.SetActive(false);
            RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
        }

    }
    public static void SpanwPlayers(string nickname1, string nickname2)
    {
        //if(nickname1 == NetPlayerTCP.GetNickname())
        //{
        //    RoomSendData.SendLoadComplite(_firstSpawnPoint.transform.position, _firstSpawnPoint.transform.rotation);
        //    _firstSpawnPoint.SpawnPlayer();
        //    _secondSpawnPoint.SpawnDummy();
        //}
        //else
        //{
        //    RoomSendData.SendLoadComplite(_secondSpawnPoint.transform.position, _secondSpawnPoint.transform.rotation);
        //    _firstSpawnPoint.SpawnDummy();
        //    _secondSpawnPoint.SpawnPlayer();
        //}
        nickname = nickname1;
        spawn = true;

    }

    public static void StartBattle()
    {
        loaded = true;
        //RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
    }
}
