using UnityEngine;
using UnityEngine.UI;

public class MainLobbyExitWindow : MonoBehaviour {

    public string thisText = "Do you really want exit to Desktop?";

    void Awake()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = thisText;
    }

    public void Yes()
    {
        MainLobbyController.DecreaseCount();
        Destroy(gameObject);
        Network.QuitApp();
    }

    public void No()
    {
        MainLobbyController.DecreaseCount();
        Destroy(gameObject);
    }
}
