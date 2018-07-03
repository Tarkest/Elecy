using UnityEngine;
using UnityEngine.UI;

public class MainLobbyErrorWindow : MonoBehaviour {

    public string thisText = "";

    void Update()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = thisText;
    }

    public void SetText(string text)
    {
        thisText = text;
    }

    public void OK()
    {
        MainLobbyController.windowsCount -= 1;
        Destroy(gameObject);
    }

}
