using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : MonoBehaviour {

    public static List<ChatMessage> glChatMessages = new List<ChatMessage>();
    private string _glCurrentMessage;
    private int newmessage = 1;

    private Text _messageHolder;
    private Text _content;
    void Start()
    {
        _messageHolder = GameObject.Find("MessageInput").GetComponentInChildren<Text>();
        _content = GameObject.Find("Content").GetComponent<Text>();
    }

    void Update()
    {
        _glCurrentMessage = _messageHolder.text;
        if (glChatMessages.Count == newmessage)
        {
            foreach(ChatMessage message in glChatMessages)
            {
                _content.text += message.ToString() + "/n";
            }
            newmessage++;
        }
    }

    public void SendChatMessage()
    {
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
