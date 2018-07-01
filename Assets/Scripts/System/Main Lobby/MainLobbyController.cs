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
    private static GameObject _exitWindow;
    private static GameObject _logOutWindow;
    private static GameObject _optionsWindow;
    private static GameObject _badConnectionPad;
    private static GameObject _errorWindow;
    private static GameObject _armoryScreen;
    private static GameObject _processScreen;
    private static int[][] ArmorySpells;

    private int matchType = 0;
    public static bool isSearching = false;
    private static float _searchTimeCounter = 0f;
    private static bool _badConnectionStatus;
    private static bool _process = false;
    private static bool _processOff = true;

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
        _exitWindow = GameObject.Find("ExitWindow");
        _logOutWindow = GameObject.Find("LogOutWindow");
        _optionsWindow = GameObject.Find("OptionsMenu");
        _badConnectionPad = GameObject.Find("BadConnectionPad");
        _errorWindow = GameObject.Find("ErrorWindow");
        _armoryScreen = GameObject.Find("ArmoryScreen");
        _errorWindow.SetActive(false);
        _badConnectionPad.SetActive(false);
        _splashMenu.SetActive(false);
        _optionsWindow.SetActive(false);
        _exitWindow.SetActive(false);
        _logOutWindow.SetActive(false);
        _armoryScreen.SetActive(false);
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
        if(_badConnectionStatus)
        {
            _badConnectionPad.SetActive(true);
        }
        else
        {
            _badConnectionPad.SetActive(false);
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
        _splashMenu.SetActive(true);
        _exitWindow.SetActive(true);
    }

    public void LogOutPressed()
    {
        _splashMenu.SetActive(true);
        _logOutWindow.SetActive(true);
    }

    public void Exit(bool answear)
    {
        if(answear)
        {
            _exitWindow.SetActive(false);
            _splashMenu.SetActive(false);
            CloseApp();
        }
        else
        {
            _exitWindow.SetActive(false);
            _splashMenu.SetActive(false);
        }
    }

    public void Options()
    {
        _optionsWindow.SetActive(true);
    }

    public void OptionsClose()
    {
        _optionsWindow.SetActive(false);
    }

    public void LogOut(bool answear)
    {
        if (answear)
        {
            _logOutWindow.SetActive(false);
            _splashMenu.SetActive(false);
            NetPlayerSendData.SendPlayerLogOut();
        }
        else
        {
            _logOutWindow.SetActive(false);
            _splashMenu.SetActive(false);
        }
    }

    public static void CloseApp()
    {
        Network.QuitApp();
    }

    public static void BadConnection(bool status)
    {
        _badConnectionStatus = status;
    }

    public static void ConnectionError()
    {
        if(_errorWindow != null)
        {
           _errorWindow.SetActive(true);
        }
        _splashMenu.SetActive(true);
    }

    public void ErrorOK()
    {
        Network.LogOut();
    }

    public void ArmoryPressed()
    {
        _armoryScreen.SetActive(true);
        NetPlayerSendData.SendGetSkillBuild("Ignis");
    }

    public void ArmorySave()
    {
        ArmoryController.SaveBuild();
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

    }
}
