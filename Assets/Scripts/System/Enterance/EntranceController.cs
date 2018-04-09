using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    public string Name;
    public string Password;
    public string Nickname;
    [System.NonSerialized]
    public static string serverInfo;
    private static Text infoField;
    private static GameObject _splashScreen;
    private static GameObject _exitWindow;
    private static GameObject _errorWindow;
    private static bool _error = false;
    private static string _errorMsg = "";

    void Start()
    {
        _splashScreen = GameObject.Find("SplashMenu");
        _exitWindow = GameObject.Find("ExitWindow");
        _errorWindow = GameObject.Find("ErrorWindow");
        _errorWindow.SetActive(false);
        _exitWindow.SetActive(false);
        _splashScreen.SetActive(false);
    }

    void Update()
    {
        Name = GameObject.Find("Name").GetComponent<InputField>().text;
        Password = GameObject.Find("Password").GetComponent<InputField>().text;
        Nickname = GameObject.Find("Nickname").GetComponent<InputField>().text;
        if(_error)
        {
            _error = false;
            _splashScreen.SetActive(true);
            _errorWindow.SetActive(true);
            _errorWindow.transform.Find("Text").GetComponent<Text>().text = _errorMsg;
            _errorMsg = "";
        }
    }

    public void LoginTry()
    {
        ClientSendData.SendLogin();
    }

    public void RegisterTry()
    {
        if(Password.Length >= 8)
        {
            ClientSendData.SendRegister();
        }
        else
        {
            GetError("Password must be at least 8 character long");
        }

    }

    public void Exit()
    {
        _splashScreen.SetActive(true);
        _exitWindow.SetActive(true);
    }

    public void Exit(bool answear)
    {
        if(answear == true)
        {
            ClientSendData.SendExit();
            _exitWindow.SetActive(false);
            _splashScreen.SetActive(false);
        }
        else
        {
            _exitWindow.SetActive(false);
            _splashScreen.SetActive(false);
        }
    }

    public void OK()
    {
        _errorWindow.SetActive(false);
        _splashScreen.SetActive(false);
    }

    public static void GetError(string errorText)
    {
        _error = true;
        _errorMsg = errorText;
    }

    public static void CloseApp()
    {
        Application.Quit();
    } 
}
