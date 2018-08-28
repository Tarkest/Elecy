using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

    private static GameObject _popUpScreen;
    private static GameObject _inGameMenu;
    private static GameObject _splashScreen;
    private static GameObject _buttons;
    private static GameObject _button;
    private static Text _text;
    private static Text _winnerText;
    private static GameObject _statisticScreen;
    private static GameObject _devScreen;

    private static bool _menuIsActive = false;
    public static bool battleIsOn = true;
    private static bool _statisticView = false;
    private static bool _won;
    private bool _devScreenIsOn;

    private static List<int> everySecond = new List<int>();

    void Awake()
    {
        _inGameMenu = GameObject.Find("InGameMenu");
        _splashScreen = GameObject.Find("SplashScreen");
        _button = GameObject.Find("Button");
        _buttons = GameObject.Find("Buttons");
        _text = _splashScreen.transform.Find("Text").GetComponent<Text>();
        _popUpScreen = GameObject.Find("PopUpScreen");
        _statisticScreen = GameObject.Find("StatisticScreen");
        _winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        _devScreen = GameObject.Find("DeveloperScreen");
    }

    void Start()
    {
        _inGameMenu.SetActive(false);
        _splashScreen.SetActive(false);
        _statisticScreen.SetActive(false);
        _popUpScreen.SetActive(false);
        _devScreen.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(battleIsOn)
            {
                if(_menuIsActive)
                {
                    _menuIsActive = false;
                    _inGameMenu.SetActive(false);
                    _popUpScreen.SetActive(false);
                }
                else
                {
                    _menuIsActive = true;
                    _popUpScreen.SetActive(true);
                    _inGameMenu.SetActive(true);
                }
            }
        }
            if (_won)
            {
                _winnerText.text = "Victory!!! Motherf**cka";
            }
            else if (!_won)
            {

                _winnerText.text = "Not today John, not today...";
            }
        if(_statisticView)
        {
            _popUpScreen.SetActive(true);
            _statisticScreen.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            if(_devScreenIsOn)
            {
                _devScreenIsOn = false;
                _devScreen.SetActive(false);
            }
            else
            {
                _devScreenIsOn = true;
                _devScreen.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        _menuIsActive = false;
        _popUpScreen.SetActive(false);
        _inGameMenu.SetActive(false);
    }

     public void Option()
    {
        Debug.Log("Y Programista Otpusk");
    }

    public void Surrender()
    {
        _inGameMenu.SetActive(false);
        _splashScreen.SetActive(true);
        _button.SetActive(false);
        _buttons.SetActive(true);
        _text.text = "Do you really want to surrender?";
    }

    public void Surrender(bool answear)
    {
        if(answear)
        {
            SendDataTCP.SendSurrender();
            _splashScreen.SetActive(false);
            _inGameMenu.SetActive(false);
            _popUpScreen.SetActive(false);
        }
        else
        {
            _splashScreen.SetActive(false);
            _inGameMenu.SetActive(true);
        }
    }

    public static void ViewStatisticScreen(string Nickname)
    {
        _statisticView = true;
        if (ClientTCP.nickname == Nickname)
        {
            _won = true;
        }
        else
        {
            _won = false;
        }
    }

    public void LeaveRoom()
    {
        SendDataTCP.SendRoomLeave();
        _inGameMenu.SetActive(false);
        _splashScreen.SetActive(false);
        _statisticScreen.SetActive(false);
        _popUpScreen.SetActive(false);
    }
}
