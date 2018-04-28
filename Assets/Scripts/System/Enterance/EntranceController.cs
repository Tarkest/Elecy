using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    public string Name;
    public string Password;
    public string Nickname;
    [System.NonSerialized]
    private static Text infoField;
    private static GameObject _splashScreen;
    private static GameObject _exitWindow;
    private static GameObject _errorWindow;
    private static GameObject _processWindow;
    private static bool _error = false;
    private static bool _process = false;
    private static string _errorMsg = "";
    private static string _processMsg = "";

    void Start()
    {
        _splashScreen = GameObject.Find("SplashMenu");
        _exitWindow = GameObject.Find("ExitWindow");
        _errorWindow = GameObject.Find("ErrorWindow");
        _processWindow = GameObject.Find("ProcessWindow");
        _processWindow.SetActive(false);
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
        if(_process)
        {
            _splashScreen.SetActive(true);
            _processWindow.SetActive(true);
            _processWindow.transform.Find("Text").GetComponent<Text>().text = _processMsg;
            _processMsg = "";
        }
    }

    public void LoginTry()
    {
        ClientSendData.SendLogin();
        GetInProcess("Login in...");
    }

    public void RegisterTry()
    {
        if(Password.Length >= 8)
        {
            ClientSendData.SendRegister();
            GetInProcess("Registration...");
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
            Network.QuitApp();
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
        GetOffProcess();
    }

    public static void GetInProcess(string processText)
    {
        _process = true;
        _processMsg = processText;
    }

    public static void GetOffProcess()
    {
        _process = false;
    }
}
