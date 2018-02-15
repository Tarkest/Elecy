using UnityEngine;
using UnityEngine.UI;

public class EntranceController : MonoBehaviour {

    public string Name;
    public string Password;

    void Update()
    {
        Name = GameObject.Find("Name").GetComponent<InputField>().text;
        Password = GameObject.Find("Password").GetComponent<InputField>().text;
    }

    public void LoginTry()
    {
        ClientTCP.SendLogin();
    }

    public void RegisterTry()
    {
        ClientTCP.SendRegister();
        
    }


}
