﻿using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    [System.NonSerialized]
    public string IP_ADDRESS;
    [System.NonSerialized]
    public int PORT = NetworkConstants.PORT;

    public bool connectToLocal;

    private static bool scenechange = false;

    private static int scenenum;

    private static bool isConnected = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        ClientHandlerNetworkData.InitializeNetworkPackages();
    }

    private void Start()
    {
        if (connectToLocal == true)
        {
            IP_ADDRESS = NetworkConstants.LOCAL_IP_ADDRESS;
        }
        else
        {
            IP_ADDRESS = NetworkConstants.IP_ADDRESS;
        }


        EntranceController.serverInfo = "Connecting to the server...";
    }

    private void Update()
    {
        if (scenechange)
        {
            scenechange = false;
            LoadScene(scenenum);
        }
        while(!isConnected)
        {
            ClientTCP.Disconnect();
            ClientTCP.Connect(IP_ADDRESS, PORT);
        }

    }

    private void LoadScene(int scenenum)
    {
        switch(scenenum)
        {
            case 1:
                SceneManager.LoadScene(NetworkConstants.MAIN_LOBBY_NUMBER);
            break;

            case 2:
                SceneManager.LoadScene(NetworkConstants.ROOM_ARENA_NUMBER);
            break;
        }

    }

    public static void ChangeConnectionStatus(bool connected)
    {
        isConnected = connected;
    }

    private void OnApplicationQuit()
    {
        ClientTCP.Close();
        NetPlayerTCP.Close();
        RoomTCP.Close();
        BattleLogic.Timer.Dispose();
    }

    public static void Login(int playerIndex, string nickname, int[][] accountData)
    {
        NetPlayerTCP.InitPlayer(playerIndex, nickname, accountData);
        NetPlayerHandleNetworkData.InitializeNetworkPackages();
        scenechange = true;
        scenenum = 1;
    }

    public static void InBattle(int roomindex)
    {
        NetPlayerSendData.SendStopPlayer();
        RoomHandleNetworkInformation.InitializeNetworkPackages();
        RoomTCP.InitRoom(roomindex);
        scenenum = 2;
        scenechange = true;
    }
}

