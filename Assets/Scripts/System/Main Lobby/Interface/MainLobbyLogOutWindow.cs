using UnityEngine;
using UnityEngine.UI;

public class MainLobbyLogOutWindow : MonoBehaviour {

    public string thisText = "Do you really want to switch account";

    void Awake()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = thisText;
    }

    public void Yes()
    {
        MainLobbyController.DecreaseCount();
        Destroy(gameObject);
        Network.LogOut();
    }

    public void No()
    {
        MainLobbyController.DecreaseCount();
        Destroy(gameObject);
    }
}
