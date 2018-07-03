using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    private string _name;
    private string _password;
    private string _nickname;
    private static GameObject _splashScreen;
    private static GameObject _errorWindow;
    private static GameObject _exitWindow;
    private static GameObject _processWindowRes;
    private static EntranceProcessWindow _processWindow;
    private static int tablesCount;

    private static bool _error;
    private static bool _exit;
    private static bool _process;
    private static bool _processOff;
    private static string _errorText;
    private static string _processText;

    void Start()
    {
        _splashScreen = GameObject.Find("SplashMenu");
        _errorWindow = Resources.Load("Entrance/ErrorWindow") as GameObject;
        _exitWindow = Resources.Load("Entrance/ExitWindow") as GameObject;
        _processWindowRes = Resources.Load("Entrance/ProcessWindow") as GameObject;
        _splashScreen.SetActive(false);
        _error = false;
        _exit = false;
        _process = false;
        tablesCount = 0;
    }

    void Update()
    {
        _name = GameObject.Find("Name").GetComponent<InputField>().text;
        _password = GameObject.Find("Password").GetComponent<InputField>().text;
        _nickname = GameObject.Find("Nickname").GetComponent<InputField>().text;

        if (tablesCount == 0)
            _splashScreen.SetActive(false);
        else
            _splashScreen.SetActive(true);

        if(_error)
        {
            _error = false;
            GameObject o = Instantiate(_errorWindow, _splashScreen.transform) as GameObject;
            o.GetComponent<EntranceErrorWindow>().SetMsg(_errorText);
        }

        if(_exit)
        {
            _exit = false;
            Instantiate(_exitWindow, _splashScreen.transform);
            Increase();
        }

        if(_process)
        {
            _process = false;
            GameObject o = Instantiate(_processWindowRes, _splashScreen.transform) as GameObject;
            _processWindow = o.GetComponent<EntranceProcessWindow>();
            _processWindow.SetText(_processText);
        }
    }

    public static void Increase()
    {
        tablesCount++;
    }

    public static void Decrease()
    {
        if(!(tablesCount <= 0))
        {
            tablesCount--;
        }
    }

    public void LoginTry()
    {
        ClientSendData.SendLogin(_name, _password);
        GetInProcess("Login in...");
    }

    public void RegisterTry()
    {
        if(_password.Length >= 8)
        {
            ClientSendData.SendRegister(_name, _password, _nickname);
            GetInProcess("Registration...");
        }
        else
        {
            GetError("Password must be at least 8 character long");
        }
    }

    public static void GetError(string errorText)
    {
        _errorText = errorText;
        _error = true;
    }

    public void Exit()
    {
        _exit = true;
    }

    public static void GetInProcess(string processText)
    {
        _processText = processText;
        _process = true;
    }

    public static void GetOffProcess()
    {
        if(_processWindow != null)
            _processWindow.Destroy();
    }
}
