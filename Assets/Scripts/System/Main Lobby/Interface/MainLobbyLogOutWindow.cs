using UnityEngine;
using UnityEngine.UI;

public class MainLobbyLogOutWindow : MonoBehaviour {

    private Text _thisTextComponent;
    public string thisText = "Do you really want to switch account";

    void Awake()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = thisText;
    }

    public void Yes()
    {
        MainLobbyController.windowsCount -= 1;
        MainLobbyController.LogOut();
        Destroy(gameObject);
    }

    public void No()
    {
        MainLobbyController.windowsCount -= 1;
        Destroy(gameObject);
    }
}
