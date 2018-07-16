using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    [System.NonSerialized]
    public static string IP_ADDRESS;
    [System.NonSerialized]
    public static int PORT = NetworkConstants.PORT;
    public static int UDP_PORT = NetworkConstants.UDP_PORT;

    public static ConnectStatus Connect;
    public static GameState state;

    public bool connectToLocal;

    private static bool scenechange;
    private static int scenenum;
    private static bool quit;
    private static GameObject _networkInstanse;

    public enum GameState
    {
        Entrance = 0,
        MainLobby = 1,
        GameArena = 2,
    }

    public enum ConnectStatus
    {
        Unconnected = 0,
        Connecting = 1,
        Connected = 2,
    }

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
        ClientHandlerNetworkData.InitializeNetworkPackages();
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
        EntranceController.GetInProcess("Connecting...");
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

    public static void LogOut()
    {
        NetPlayerTCP.Close();
        ClientTCP.Close();
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

    public static void InBattle(int mapIndex)
    {
        RoomHandleNetworkInformation.InitializeNetworkPackages();
        RoomTCP.InitRoom();
        scenenum = mapIndex;
        scenechange = true;
    }

    public static void QuitApp()
    {
        quit = true;
    }

    private void LoadScene(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
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
}

