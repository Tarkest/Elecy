using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

    protected internal static GameObject _popUpScreen;
    protected internal static GameObject _inGameMenu;
    protected internal static GameObject _splashScreen;
    protected internal static GameObject _buttons;
    protected internal static GameObject _button;
    protected internal static Text _text;
    protected internal static Text _winnerText;
    protected internal static GameObject _statisticScreen;
    protected internal static GameObject _devScreen;

    protected internal static bool _menuIsActive = false;
    public static bool battleIsOn = true;
    protected internal static bool _statisticView = false;
    protected internal static bool _won;
    protected internal bool _devScreenIsOn;

    protected void Awake()
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

    protected void Start()
    {
        _inGameMenu.SetActive(false);
        _splashScreen.SetActive(false);
        _statisticScreen.SetActive(false);
        _popUpScreen.SetActive(false);
        _devScreen.SetActive(false);
    }

    protected void Update()
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
