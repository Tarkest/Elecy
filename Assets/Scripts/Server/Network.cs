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
        BattleLogic.Timer.Dispose();
    }
    public static void LogOut()
    {
        scenenum = 0;
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

