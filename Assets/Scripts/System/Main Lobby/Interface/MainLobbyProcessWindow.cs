using UnityEngine;
using UnityEngine.UI;

public class MainLobbyProcessWindow : MonoBehaviour {

    private bool _destroy;

    void Update()
    {
        if(_destroy)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeText(string text)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = text;
    }

    public void GetOffProcess()
    {
        _destroy = true;
    }
}
