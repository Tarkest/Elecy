using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{

    #region Variables
    public static string IP_ADDRESS;
    public static int PORT = NetworkConstants.PORT;
    public static int UDP_PORT = NetworkConstants.UDP_PORT;
    public static ConnectStatus Connect;

    public bool connectToLocal;

    private static bool scenechange;
    private static bool quit;
    private static int scenenum;
    private static GameObject _networkInstanse;
    #endregion

    #region Enum

    public enum ConnectStatus
    {
        Unconnected = 0,
        Connecting = 1,
        Connected = 2,
    }

    #endregion

    #region Unity's

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_networkInstanse == null)
        {
            _networkInstanse = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        Connect = ConnectStatus.Unconnected;
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
        HandleDataTCP.InitializeNetworkPackages();
        ClientTCP.Init();
    }

    private void Update()
    {
        if (scenechange)
        {
            scenechange = false;
            LoadScene(scenenum);
        }
        if(Connect == ConnectStatus.Connecting)
        {
            ClientTCP.Connect(IP_ADDRESS, PORT);
        }
        if(quit)
        {
            quit = false;
            Application.Quit();
        }
    }

    #endregion

    #region Transitions

    public static void Login(int playerIndex, string nickname, int[][] accountData)
    {
        ClientTCP.Login(nickname, accountData);
        scenenum = 1;
        scenechange = true;
    }

    public static void LogOut()
    {
        ClientTCP.Close();
        scenenum = 0;
        scenechange = true;
    }

    public static void InBattle()
    {
        scenenum = 2;
        scenechange = true;
    }

    public static void EndBattle()
    {
        ClientTCP.LeaveRoom();
        scenenum = 1;
        scenechange = true;
    }

    private void LoadScene(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
    }

    #endregion

    #region Quit

    public static void QuitApp()
    {
        quit = true;
    }

    private void OnApplicationQuit()
    {
        ClientTCP.Close();
        try
        {
            BattleLogic.StopTimer(true);
        }
        catch { }
    }

    #endregion

}

