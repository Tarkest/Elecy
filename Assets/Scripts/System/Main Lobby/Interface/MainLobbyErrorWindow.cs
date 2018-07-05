using UnityEngine;
using UnityEngine.UI;

public class MainLobbyErrorWindow : MonoBehaviour
{

    public void SetText(string text)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        MainLobbyController.IncreaseCount();
    }

    public void OK()
    {
        MainLobbyController.DecreaseCount();
        Destroy(gameObject);
    }

}
