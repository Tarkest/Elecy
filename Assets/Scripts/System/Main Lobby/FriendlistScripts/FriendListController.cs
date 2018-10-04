using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendListController : MonoBehaviour {

    public static GameObject _friend;
    private static RectTransform _content;
    private static int _currentFriendsCount;
    private static Dictionary<string, FriendFieldController> Friends = new Dictionary<string, FriendFieldController>();
    private static InputField _tagHolder;

    void Awake()
    {
        _content = gameObject.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        _tagHolder = gameObject.transform.Find("TagHolder").GetComponent<InputField>();
        _friend = Resources.Load("Interface/MainLobbyInterface/Friend") as GameObject;
        _currentFriendsCount = 0;
    }

    void Update()
    {
        _content.sizeDelta = new Vector2(0, (float)60 * _currentFriendsCount);
    }

    public static void AddFriends(string[] nickname, string[] guidekey, int[] status)
    {
        for(int i = 0; i<guidekey.Length; i++)
        {
            _currentFriendsCount += 1;
            GameObject friend = Instantiate(_friend, _content.transform);
            friend.GetComponent<FriendFieldController>().AddFriend(nickname[i], status[i]);
            Friends.Add(guidekey[i], friend.GetComponent<FriendFieldController>());
        }
        MainLobbyController.DestroyBlackScreen();
    }

    public static void AddFriend(string nickname, string guidekey, int status)
    {
        _currentFriendsCount += 1;
        GameObject friend = Instantiate(_friend, _content.transform);
        friend.GetComponent<FriendFieldController>().AddFriend(nickname, status);
        Friends.Add(guidekey, friend.GetComponent<FriendFieldController>());
    }

    public static void ChangeStatus(string guidekey, int status)
    {
        FriendFieldController value;
        if(Friends.TryGetValue(guidekey, out value))
            value.ChangeStatus(status);
    }

    public static void DestroyFriend(string guidekey)
    {
        FriendFieldController value;
        if (Friends.TryGetValue(guidekey, out value))
        {
            Friends.Remove(guidekey);
            MainThread.executeInUpdate(()=>Destroy(value.gameObject));
        }
    }

    public void SendFriendTag()
    {
        SendDataTCP.SendFriendTag(_tagHolder.text);
    }
}
