using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendListController : MonoBehaviour {

    private static RectTransform _content;
    private static int _currentFriendsCount;

    void Awake()
    {
        _content = gameObject.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        _currentFriendsCount = 0;
    }

    void Update()
    {
        _content.sizeDelta = new Vector2(0, (float)75 * _currentFriendsCount);
    }

    public static void AddFriend()
    {

    }
}
