using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalChatController : MonoBehaviour {

    private static RectTransform _content;
    private InputField _messageInput;
    private static int _messageCount;

    void Awake()
    {
        _content = gameObject.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        _messageInput = gameObject.transform.Find("MessageInput").GetComponent<InputField>();
        _messageCount = 0;
    }

    void Update()
    {
        _content.sizeDelta = new Vector2(0, (float)16 * _messageCount);
    }
    
    void SendMessage()
    {
        ClientSendData.SendGlChatMsg(_messageInput.text);
        _messageInput.text = "";
    } 
    
    public static void RecieveMessage(string nickname, string message)
    {
        _messageCount++;
        GameObject NewMessage = Instantiate(Resources.Load("MainLobby/GlobalChat/ChatMessageParent"), _content) as GameObject;
        NewMessage.GetComponent<ChatMessageController>().AddMessage(nickname, message);
    } 
}
