using UnityEngine;
using UnityEngine.UI;

public class MainLobbyExitWindow : MonoBehaviour {

    private Text _thisTextComponent;
    public string thisText = "Do you really want exit to Desktop?";

    void Awake()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = thisText;
    }

    public void Yes()
    {
        MainLobbyController.windowsCount -= 1;
        MainLobbyController.Exit();
        Destroy(gameObject);
    }

    public void No()
    {
        MainLobbyController.windowsCount -= 1;
        Destroy(gameObject);
    }
}
