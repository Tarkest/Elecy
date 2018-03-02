using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : MonoBehaviour {

    public static List<ChatMessage> glChatMessages = new List<ChatMessage>();
    private string _glCurrentMessage;
    private int newmessage = 1;
    private static float _fieldSize;

    private static RectTransform fieldSize;
    private static InputField _messageHolder;
    private Text _content;
    void Start()
    {
        fieldSize = GameObject.Find("Content").GetComponent<RectTransform>();
        _fieldSize = 300;
        _messageHolder = GameObject.Find("MessageInput").GetComponent<InputField>();
        _content = GameObject.Find("Content").GetComponent<Text>();
    }

    void Update()
    {
        fieldSize.sizeDelta = new Vector2(0, _fieldSize);
        //_glCurrentMessage = _messageHolder.text;
        if (glChatMessages.Count == newmessage)
        {
            _content.text = "";
            foreach(ChatMessage message in glChatMessages)
            {
                _content.text += "\n" + message.ToString();
            }
            newmessage++;
            _fieldSize += 20f;
        }
    }

    public void SendChatMessage()
    {
        _glCurrentMessage = _messageHolder.text;
        ClientSendData.SendGlChatMsg(_glCurrentMessage);
        _glCurrentMessage = null;
    }
}

public class ChatMessage 
{
    private string message;
    private string nickname;

    public ChatMessage(string nickname, string message)
    {
        this.nickname = nickname;
        this.message = message;
    }

    public override string ToString()
    {
        return nickname + ": " + message;
    }
}
