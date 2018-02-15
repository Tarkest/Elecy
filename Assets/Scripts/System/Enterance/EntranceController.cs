using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    public string Name;
    public string Password;
    private int infoNum;
    private static Text infoField;

    void Start()
    {
        infoField = GameObject.Find("InfoField").GetComponent<Text>();
    }

    void Update()
    {
        Name = GameObject.Find("Name").GetComponent<InputField>().text;
        Password = GameObject.Find("Password").GetComponent<InputField>().text;
    }

    public void LoginTry()
    {
        ClientSendData.SendLogin();
    }

    public void RegisterTry()
    {
        ClientSendData.SendRegister();
    }

    public static void TextInfo(int infonum)
    {
        string[] info = new string[7];

        info[0] = "Connecting to the server...";
        info[1] = "Connect established";
        info[2] = "Singing up new account...";
        info[3] = "Account registered";
        info[4] = "Loggin in...";
        info[5] = "Login succesfull";
        info[6] = "Connection failed";

        infoField.text = info[infonum];
    }


}
