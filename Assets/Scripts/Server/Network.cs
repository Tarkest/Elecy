using UnityEngine;
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

    public static bool quit = false;

    public static GameState state;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        ClientHandlerNetworkData.InitializeNetworkPackages();
    }

    public enum GameState
    {
        Entrance = 0,
        MainLobby = 1,
        GameArena = 2,
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
        EntranceController.GetInProcess("Connecting...");
    }

    private void Update()
    {
        if (scenechange)
        {
            scenechange = false;
            LoadScene(scenenum);
        }
        if(!isConnected)
        {
            ClientTCP.Connect(IP_ADDRESS, PORT);
        }
        if(quit)
        {
            quit = false;
            Application.Quit();
        }
    }
    // SceneManger
    private void LoadScene(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
    }

    public static void ChangeConnectionStatus(bool connected)
    {
        isConnected = connected;
    }

    public static void QuitApp()
    {
        quit = true;
    }

    private void OnApplicationQuit()
    {
        ClientTCP.Close();
        NetPlayerTCP.Close();
        RoomTCP.Close();

        try
        {
            BattleLogic.Timer.Dispose();
        }
        catch { }
    }

    public static void LogOut()
    {
        scenenum = 0;
        scenechange = true;
    }

    public static void EndBattle()
    {
        scenenum = 1;
        scenechange = true;
    }

    public static void Login(int playerIndex, string nickname, int[][] accountData)
    {
        NetPlayerTCP.InitPlayer(playerIndex, nickname, accountData);
        NetPlayerHandleNetworkData.InitializeNetworkPackages();
        scenenum = 1;
        scenechange = true;
    }

    public static void InBattle(int roomindex, int mapIndex)
    {
        RoomHandleNetworkInformation.InitializeNetworkPackages();
        RoomTCP.InitRoom(roomindex);
        scenenum = mapIndex;
        scenechange = true;
    }
}

