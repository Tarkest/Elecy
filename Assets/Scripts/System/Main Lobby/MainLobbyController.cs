using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : MonoBehaviour
{

    #region Public Properties

    public static bool isSearching;
    public static int windowsCount;
    public static float searchTimeCounter { get; private set; }

    #endregion

    #region Private Members

    private static GameObject _testRoomButton;
    private static Dropdown _testRoomDropdown;
    private static Dictionary<string, GameObject> _dropOptions;

    private static GameObject _findGameButton;
    private static GameObject _armoryButton;
    //private GameObject _shopButton; Button For go into Shop Window
    private static GameObject _machTypeDropdown;
    private static GameObject _timeCounter;
    private static GameObject _splashMenu;
    private static GameObject _optionsWindow;
    private static GameObject _badConnectionPad;
    private static GameObject _armoryScreen;
    private static GameObject _processScreen;
    private static MainLobbyProcessWindow _processWindow;
    private static GameObject _errorWindow;
    private static int[][] ArmorySpells;

    private int matchType = 0;

    private static bool _process = false;
    private static bool _error;
    private static string _errorMsg;
    private static string _processText;

    #endregion

    #region Unity's

    void Awake()
    {
        _findGameButton = GameObject.Find("FindGameButton");
        _armoryButton = GameObject.Find("ArmoryButton");
        //_shopButton = GameObject.Find("ShopButton");
        _machTypeDropdown = GameObject.Find("MatchTypeDropdown");
        _timeCounter = GameObject.Find("TimeCounter");
        _splashMenu = GameObject.Find("SplashMenu");
        _optionsWindow = GameObject.Find("OptionsMenu");
        _badConnectionPad = GameObject.Find("BadConnectionPad");
        _armoryScreen = GameObject.Find("ArmoryScreen");
        _processScreen = GameObject.Find("ProcessPanel");

        #region TestRoom

        _testRoomButton = GameObject.Find("TestRoomButton");
        _testRoomDropdown = GameObject.Find("TestRoomDropdown").GetComponent<Dropdown>();
        _dropOptions = new Dictionary<string, GameObject>();
        _dropOptions.Add("random", null);
        for(int i = 1; i > 0; i++)
        {
            GameObject map = Resources.Load("Maps/" + i + "/GameArea") as GameObject;
            if (map != null)
                _dropOptions.Add(i.ToString(), map);
            else
                i = -1;
        }
        _testRoomDropdown.ClearOptions();
        _testRoomDropdown.AddOptions(new List<string>(_dropOptions.Keys));

        #endregion

    }

    void Start()
    {
        _badConnectionPad.SetActive(false);
        _splashMenu.SetActive(false);
        _optionsWindow.SetActive(false);
        _armoryScreen.SetActive(false);
        _processScreen.SetActive(false);
        isSearching = false;
        windowsCount = 0;
        searchTimeCounter = 0f;
    }

    void Update()
    {
        if(isSearching)
        {
            searchTimeCounter += Time.deltaTime;
            _timeCounter.GetComponent<Text>().text = TimeInText(searchTimeCounter);
            _machTypeDropdown.SetActive(false);
            _findGameButton.transform.Find("Text").GetComponent<Text>().text = "Searching...";
        }
        else
        {
            _machTypeDropdown.SetActive(true);
            _findGameButton.transform.Find("Text").GetComponent<Text>().text = "Find Game";
        }
        if(windowsCount > 0)
        {
            _splashMenu.SetActive(true);
        }
        else
        {
            _splashMenu.SetActive(false);
        }
        if(_error)
        {
            _error = false;
            GameObject error = Resources.Load("Interface/MainLobbyInterface/ErrorWindow") as GameObject;
            error.GetComponent<MainLobbyErrorWindow>().SetText(_errorMsg);
            Instantiate(error, _splashMenu.transform);
        }
        if(_process)
        {
            _process = false;
            _processScreen.SetActive(true);
            _processWindow = Instantiate(Resources.Load("Interface/MainLobbyInterface/ProcessWindow") as GameObject, _processScreen.transform).GetComponent<MainLobbyProcessWindow>();
            _processWindow.ChangeText(_processText);
        }
        if(_processScreen.activeSelf && _processWindow == null)
        {
            _processScreen.SetActive(false);
        }
    }

    #endregion

    #region TimeToTextConverter

    private string TimeInText(float Time)
    {
        int currentTime = (int)Time;
        int minutes = currentTime / 60;
        int seconds = currentTime - minutes * 60;
        string secString;
        if (seconds < 10)
            secString = "0" + seconds.ToString();
        else
            secString = seconds.ToString();
        if (minutes == 0)
            return secString;
        else
            return minutes.ToString() + ":" + secString;
    }

    #endregion

    #region Button Events

    public void ChangeMatchType()
    {
        switch(matchType)
        {
            case 0:
                matchType = 1;
            break;

            case 1:
                matchType = 0;
            break;
        }
    }

    public void StartSearchingForMatch()
    {
        if(!isSearching)
        { 
            searchTimeCounter = 0;
            isSearching = true;
            SendDataTCP.SendQueueStart(matchType, "Ignis");
        }
        else if(isSearching)
        {
            isSearching = false;
            _timeCounter.GetComponent<Text>().text = "";
            SendDataTCP.SendQueueStop();
        }
    }

    public void ExitPressed()
    {
        IncreaseCount();
        Instantiate(Resources.Load("Interface/MainLobbyInterface/ExitWindow"), _splashMenu.transform);  
    }

    public void LogOutPressed()
    {
        IncreaseCount();
        Instantiate(Resources.Load("Interface/MainLobbyInterface/LogOutWindow"), _splashMenu.transform);
    }

    public void Options()
    {
        _optionsWindow.SetActive(true);
    }

    public void OptionsClose()
    {
        _optionsWindow.SetActive(false);
    }

    public void ArmoryPressed()
    {
        _armoryButton.SetActive(false);
        _armoryScreen.SetActive(true);
        SendDataTCP.SendGetSkillBuild("Ignis");
    }

    public void ArmorySave()
    {
        ArmoryController.SaveBuild();
        GetInProcess("Saving Build");
    }

    public void ArmoryBack()
    {
        _armoryButton.SetActive(true);
        _armoryScreen.SetActive(false);
    }

    public void TestRoom_Click()
    {
        SendDataTCP.SendTestRoomEnter(_testRoomDropdown.value, "Ignis");
        _testRoomButton.GetComponent<Button>().interactable = false;
        _testRoomDropdown.interactable = false;
    }

    #endregion

    public static void IncreaseCount()
    {
        windowsCount++;
    }
    
    public static void DecreaseCount()
    {
        if(windowsCount > 0)
        {
            windowsCount--;
        }
    }

    public static void Error(string ErrorMassage)
    {
        _errorMsg = ErrorMassage;
        _error = true;
    }

    public static void GetInProcess(string processName)
    {
        _processText = processName;
        _process = true;
    }

    public static void GetOffProcess()
    {
        _processWindow.GetOffProcess();
    }
}
