using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessageController : MonoBehaviour {

    private RectTransform _nicknameTransform;
    private Text _nicknameText;
    private RectTransform _messageTransform;
    private Text _messageText;

    private string _nicknameTextBuffer;
    private string _messageTextBuffer;

    void AwakeMessage()
    {
        _nicknameTransform = gameObject.transform.Find("NicknameButton").GetComponent<RectTransform>();
        _nicknameText = gameObject.transform.Find("NicknameButton").Find("NicknameText").GetComponent<Text>();
        _messageTransform = gameObject.transform.Find("MessageText").GetComponent<RectTransform>();
        _messageText = gameObject.transform.Find("MessageText").GetComponent<Text>();
        CreateMessage();
    }

    public void CreateMessage()
    {
        _nicknameTransform.sizeDelta = new Vector2(((float)(_nicknameTextBuffer.Length + 2) * (_nicknameText.fontSize/2f)), 16);
        _messageTransform.sizeDelta = new Vector2((580f - (float)_nicknameTextBuffer.Length * (_nicknameText.fontSize/2f)), 16);
        _nicknameText.text = " " + _nicknameTextBuffer + ": ";
        _messageText.text = _messageTextBuffer;  
    }

    public void AddMessage(string nickname, string message)
    {
        _nicknameTextBuffer = nickname;
        _messageTextBuffer = message;
        AwakeMessage();
    }

}
