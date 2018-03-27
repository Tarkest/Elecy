using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    public string Name;
    public string Password;
    public string Nickname;
    [System.NonSerialized]
    public static string serverInfo;
    private static Text infoField;

    void Start()
    {
        infoField = GameObject.Find("InfoField").GetComponent<Text>();
    }

    void Update()
    {
        Name = GameObject.Find("Name").GetComponent<InputField>().text;
        Password = GameObject.Find("Password").GetComponent<InputField>().text;
        Nickname = GameObject.Find("Nickname").GetComponent<InputField>().text;
        infoField.text = serverInfo;
    }

    public void LoginTry()
    {
        ClientSendData.SendLogin();
    }

    public void RegisterTry()
    {
        ClientSendData.SendRegister();
    }
}
