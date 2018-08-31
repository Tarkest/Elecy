using System;
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
    public static string currentRace = "Ignis";
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
            //if (Input.GetMouseButtonDown(0))
            //{
            //    NetworkInstantiate(currentManager.prefabList[0], MouseController.mousePosition, Quaternion.identity);
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    NetworkInstantiate(currentManager.prefabList[1], MouseController.mousePosition, Quaternion.identity);
            //}
            //if (Input.GetMouseButtonDown(2))
            //{
            //    NetworkInstantiate(currentManager.prefabList[2], MouseController.mousePosition, Quaternion.identity);
            //}
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

    public static void InBattle(int sceneNum)
    {
        scenenum = sceneNum;
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

    public static void NetworkInstantiate(GameObject Object, Vector3? CastPosition = null, Vector3? TargetPosition = null, Quaternion? Rotation = null, Transform Parent = null)
    {
        if (currentManager.prefabList.Contains(Object))
        {
            int _index = currentManager.prefabList.IndexOf(Object);
            int _parentIndex = Parent != null ? Parent.gameObject.GetComponent<Spell>().index : -1;
            Vector3 castPos = CastPosition ?? Vector3.zero;
            Vector3 targetPos = TargetPosition ?? Vector3.zero;
            Quaternion rot = Rotation ?? Quaternion.identity;

            float[] _castPos = new float[3] { castPos.x, castPos.y == 0 ? 0.5f : castPos.y, castPos.z };
            float[] _targetPos = new float[3] { targetPos.x, targetPos.y == 0 ? 0.5f : targetPos.y, targetPos.z };
            float[] _rot = new float[4] { rot.x, rot.y, rot.z, rot.w };
            SendDataTCP.SendInstantiate(_index, _parentIndex, _castPos,_targetPos, _rot, (Object.GetComponent<SpellStats>().stats as SpellMenu).SpellMaxHP);
        }
        else
        {
            throw new Exception("В предзагрузке отсутсвует обьект");
        }
    }

    public static void NetworkInstantiate(int PrefabIndex, int ObjectIndex, int InstanceIndex, float[] CastPosition, float[] TargetPosition, float[] Rotation, int HP, string Owner)
    {
        GameObject _prefab = currentManager.prefabList[PrefabIndex];
        Vector3 _castPosition = new Vector3(CastPosition[0], CastPosition[1], CastPosition[2]);
        Vector3 _targetPosition = new Vector3(TargetPosition[0], TargetPosition[1], TargetPosition[2]);
        Quaternion _rotation = new Quaternion(Rotation[0], Rotation[1], Rotation[2], Rotation[3]);
        if(currentManager.Players.Length == 1)
        {
            InstantiateTestSpell(_prefab, _castPosition, _targetPosition, _rotation, ObjectIndex, InstanceIndex, Owner == ClientTCP.nickname);
        }
        else
        {
           InstantiateSpell(_prefab, _castPosition, _targetPosition, _rotation, ObjectIndex, InstanceIndex, Owner == ClientTCP.nickname);
        }
    }

    #endregion

    #region Private Helpers

    private static void InstantiateSpell(GameObject prefab, Vector3 castPosition, Vector3 targetPosition, Quaternion rotation, int index, int parentIndex, bool isMain)
    {
        GameObject _instance;
        if (parentIndex != -1)
        {
            Transform _parent = currentManager.dynamicPropList.Get(parentIndex).transform;
            _instance =  Instantiate(prefab, castPosition, rotation, _parent);
        }
        else
        {
           _instance = Instantiate(prefab, castPosition, rotation);
        }
        Spell baseComponent = _instance.GetComponent<Spell>();
        baseComponent.SetStartProperties(castPosition,
                                        targetPosition,
                                        index,
                                        isMain);
        currentManager.dynamicPropList.Add(baseComponent, index);
    }

    private static void InstantiateTestSpell(GameObject prefab, Vector3 castPosition, Vector3 targetPosition, Quaternion rotation, int index, int parentIndex, bool isMain)
    {
        GameObject test_instance;
        if (parentIndex != -1)
        {
            Transform _parent = currentManager.dynamicPropList.Get(parentIndex).transform;
            test_instance = Instantiate(Resources.Load("Spells/TestSpell"), Vector3.zero, Quaternion.identity, _parent) as GameObject;
        }
        else
        {
            test_instance = Instantiate(Resources.Load("Spells/TestSpell"), Vector3.zero, Quaternion.identity) as GameObject;
        }
        TestSpell baseComponent = test_instance.GetComponent<TestSpell>();
        baseComponent.SetStartProperties(prefab, castPosition, rotation, targetPosition, index, isMain);
        currentManager.dynamicPropList.Add(baseComponent, index);
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

