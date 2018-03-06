using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour {

    private static SpawnPoint _firstSpawnPoint;
    private static SpawnPoint _secondSpawnPoint;
    private static GameObject _loadScreen;

    private void Awake()
    {
        _firstSpawnPoint = gameObject.transform.Find("SpawnPoint1").GetComponent<SpawnPoint>();
        _secondSpawnPoint = gameObject.transform.Find("SpawnPoint2").GetComponent<SpawnPoint>();
        _loadScreen = GameObject.Find("LoadScreen");
    }

    public static void SpanwPlayers(string nickname1, string nickname2)
    {
        if(nickname1 == NetPlayerTCP.GetNickname())
        {
            RoomSendData.SendLoadComplite(_firstSpawnPoint.transform.position, _firstSpawnPoint.transform.rotation);
            _firstSpawnPoint.SpawnPlayer();
            _secondSpawnPoint.SpawnDummy();
        }
        else
        {
            RoomSendData.SendLoadComplite(_secondSpawnPoint.transform.position, _secondSpawnPoint.transform.rotation);
            _firstSpawnPoint.SpawnDummy();
            _secondSpawnPoint.SpawnPlayer();
        }
    }

    public static void StartBattle()
    {
        _loadScreen.SetActive(false);
        RoomSendData.SendTransform(GameObject.Find("TestPlayer").GetComponent<Transform>().position, GameObject.Find("TestPlayer").GetComponent<Transform>().rotation);
    }
}
