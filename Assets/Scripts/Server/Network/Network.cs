using System;
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
    public static ObjectManager currentManager;
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
        if(currentManager != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                NetworkInstantiate(currentManager.prefabList[0], MouseController.mousePosition, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                NetworkInstantiate(currentManager.prefabList[1], MouseController.mousePosition, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(2))
            {
                NetworkInstantiate(currentManager.prefabList[2], MouseController.mousePosition, Quaternion.identity);
            }
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

    #region Ingame
    
    public static void SetManager(ObjectManager manager)
    {
        currentManager = manager;
    }

    public static void NetworkInstantiate(GameObject Object, Vector3? Position = null, Quaternion? Rotation = null, Transform parent = null)
    {
        if (currentManager.prefabList.Contains(Object))
        {
            int _index = currentManager.prefabList.IndexOf(Object);
            int _parentIndex;
            Vector3 pos = Position ?? Vector3.zero;
            Quaternion rot = Rotation ?? Quaternion.identity;
            float[] _pos = new float[3] { pos.x, pos.y, pos.z };
            float[] _rot = new float[4] { rot.x, rot.y, rot.z, rot.w };
            if(parent != null)
            {
               _parentIndex = parent.gameObject.GetComponent<NetworkObjectController>().index;
            }
            else
            {
                _parentIndex = -1;
            }
            SendDataTCP.SendInstantiate(_index, _parentIndex, _pos, _rot, Object.GetComponent<NetworkObjectController>().hp);
        }
        else
        {
            throw new Exception("В предзагрузке отсутсвует обьект");
        }
    }

    public static void NetworkInstantiate(int PrefabIndex, int ObjectIndex, int InstanceIndex, float[] Position, float[] Rotation, int HP, string Owner)
    {
        GameObject _prefab = currentManager.prefabList[PrefabIndex];
        Vector3 _position = new Vector3(Position[0], Position[1], Position[2]);
        Quaternion _rotation = new Quaternion(Rotation[0], Rotation[1], Rotation[2], Rotation[3]);
        GameObject _instance;
        if (InstanceIndex != -1)
        {
            Transform _parent = currentManager.dynamicPropList.Get(InstanceIndex).transform;
            _instance = Instantiate(_prefab, _position, _rotation, _parent);
        }
        else
        {
            _instance = Instantiate(_prefab, _position, _rotation);
        }
        _instance.GetComponent<NetworkObjectController>().hp = HP;
        if (Owner == ClientTCP.nickname)
        {
            _instance.GetComponent<NetworkObjectController>().owner = true;
        }
        currentManager.dynamicPropList.Add(_instance.GetComponent<NetworkObjectController>(), ObjectIndex);
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

