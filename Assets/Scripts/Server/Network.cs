using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    [System.NonSerialized]
    public string IP_ADDRESS;
    [System.NonSerialized]
    public int PORT = 24985;

    public bool connectToLocal;

    private static bool scenechange = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        ClientHandlerNetworkData.InitializeNetworkPackages();
    }

    private void Start()
    {
        if (connectToLocal == true)
        {
            IP_ADDRESS = "127.0.0.1";
        }
        else
        {
            IP_ADDRESS = "77.122.14.86";
        }

        ClientTCP.Connect(IP_ADDRESS, PORT);
        EntranceController.serverInfo = "Connecting to the server...";
    }

    private void Update()
    {
        if (scenechange)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        scenechange = false;
        SceneManager.LoadScene(2);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            NetPlayerTCP.BeginReceive();
        }
    }

    private void OnApplicationQuit()
    {
        if (ClientTCP.isConnected())
            ClientSendData.SendClose();
        ClientTCP.Close();
    }

    public static void Login(int playerIndex, string nickname, int[][] accountData)
    {
        NetPlayerTCP.InitPlayer(playerIndex, nickname, accountData);
        NetPlayerHandleNetworkData.InitializeNetworkPackages();
        scenechange = true;
    }

}

