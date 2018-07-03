using UnityEngine;
using UnityEngine.UI;

public class MainLobbyProcessWindow : MonoBehaviour {

    private string _text;
    private bool _destroy;

    void Update()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = _text;
        if(_destroy)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeText(string text)
    {
        _text = text;
    }

    public void GetOffProcess()
    {
        _destroy = true;
    }
}
