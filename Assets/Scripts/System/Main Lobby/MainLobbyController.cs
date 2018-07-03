using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : MonoBehaviour {

    private static GameObject _findGameButton;
    private GameObject _armoryButton;
    //private GameObject _shopButton; Button For go into Shop Window
    private static GameObject _machTypeDropdown;
    private static GameObject _timeCounter;
    private static GameObject _splashMenu;
    private static GameObject _optionsWindow;
    private static GameObject _badConnectionPad;
    private static GameObject _armoryScreen;
    private static GameObject _processScreen;
    private static MainLobbyProcessWindow _processWindow;
    private static int[][] ArmorySpells;

    private int matchType = 0;
    public static bool isSearching = false;
    private static float _searchTimeCounter = 0f;
    private static bool _process = false;
    public static int windowsCount = 0;
    private static bool _error;
    private static string _errorMsg;
    private static string _processText;

    void Awake()
    {
        _findGameButton = GameObject.Find("FindGameButton");
        _armoryButton = GameObject.Find("ArmoryButton");
        //_shopButton = GameObject.Find("ShopButton");
        _machTypeDropdown = GameObject.Find("MatchTypeDropdown");
        _timeCounter = GameObject.Find("TimeCounter");
    }

    void Start()
    {
        _splashMenu = GameObject.Find("SplashMenu");
        _optionsWindow = GameObject.Find("OptionsMenu");
        _badConnectionPad = GameObject.Find("BadConnectionPad");
        _armoryScreen = GameObject.Find("ArmoryScreen");
        _processScreen = GameObject.Find("ProcessPanel");
        _badConnectionPad.SetActive(false);
        _splashMenu.SetActive(false);
        _optionsWindow.SetActive(false);
        _armoryScreen.SetActive(false);
        _processScreen.SetActive(false);
    }

    public static float GetCounter()
    {
        return _searchTimeCounter;
    }

    void Update()
    {
        if(isSearching)
        {
            _searchTimeCounter += Time.deltaTime;
            _timeCounter.GetComponent<Text>().text = TimeInText(_searchTimeCounter);
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
            GameObject error = Resources.Load("Interface/MainLobbyInterface/ErrorWindow") as GameObject;
            error.GetComponent<MainLobbyErrorWindow>().SetText(_errorMsg);
            _errorMsg = "";
            Instantiate(error, _splashMenu.transform);
            _error = false;
        }
        if(_process)
        {
            _process = false;
            _processScreen.SetActive(true);
            _processWindow = Instantiate(Resources.Load("Interface/MainLobbyInterface/ProcessWindow") as GameObject, _processScreen.transform).GetComponent<MainLobbyProcessWindow>();
            _processWindow.ChangeText(_processText);
            _processText = "";
        }
    }

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
            _searchTimeCounter = 0;
            isSearching = true;
            NetPlayerSendData.SendQueueStart(matchType, "Ignis");
        }
        else if(isSearching)
        {
            isSearching = false;
            _timeCounter.GetComponent<Text>().text = "";
            NetPlayerSendData.SendQueueStop();
        }
    }

    public void ExitPressed()
    {
        windowsCount += 1;
        Instantiate(Resources.Load("Interface/MainLobbyInterface/ExitWindow"), _splashMenu.transform);  
    }

    public void LogOutPressed()
    {
        windowsCount += 1;
        Instantiate(Resources.Load("Interface/MainLobbyInterface/LogOutWindow"), _splashMenu.transform);
    }

    public static void Exit()
    {
        Network.QuitApp();
    }

    public void Options()
    {
        _optionsWindow.SetActive(true);
    }

    public void OptionsClose()
    {
        _optionsWindow.SetActive(false);
    }

    public static void LogOut()
    {
        NetPlayerSendData.SendPlayerLogOut();
    }

    public static void Error(string ErrorMassage)
    {
        windowsCount += 1;
        _error = true;
        _errorMsg = ErrorMassage;
    }

    public void ArmoryPressed()
    {
        _armoryScreen.SetActive(true);
        NetPlayerSendData.SendGetSkillBuild("Ignis");
    }

    public void ArmorySave()
    {
        ArmoryController.SaveBuild();
        GetInProcess("Saving Build");
    }

    public void ArmoryBack()
    {
        _armoryScreen.SetActive(false);
    }

    public static void LoadSpells(int[] spellsNumbers)
    {
        //Change
    }

    public static void GetInProcess(string processName)
    {
        _process = true;
        _processText = processName;
    }

    public static void GetOffProcess()
    {
        _processWindow.GetOffProcess();
    }
}
