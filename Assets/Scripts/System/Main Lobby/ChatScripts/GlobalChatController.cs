using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalChatController : MonoBehaviour {

    private static RectTransform _content;
    private InputField _messageInput;
    private static int _messageCount;

    private static bool msgRecieve = false;
    private static string _currentMsg;
    private static string _currentNick;

    void Awake()
    {
        _content = gameObject.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        _messageInput = gameObject.transform.Find("MessageInput").GetComponent<InputField>();
        _messageCount = 0;
    }

    void Update()
    {
        _content.sizeDelta = new Vector2(0, (float)16 * _messageCount);
        if(msgRecieve)
        {
            msgRecieve = false;
            GameObject NewMessage = Instantiate(Resources.Load("MainLobby/GlobalChat/ChatMessageParent"), _content) as GameObject;
            NewMessage.GetComponent<ChatMessageController>().AddMessage(_currentNick, _currentMsg);
            _currentMsg = null;
            _currentNick = null;
        }
    }
    
    public void SendChatMessage()
    {
        ClientSendData.SendGlChatMsg(_messageInput.text);
        _messageInput.text = "";
    } 
    
    public static void RecieveMessage(string nickname, string message)
    {
        _messageCount++;
        _currentMsg = message;
        _currentNick = nickname;
        msgRecieve = true;
    } 
}
