using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendFieldController : MonoBehaviour {

    private Image Avatar;
    private Text NickName;
    private Text Status;

    private string _NicknameBuffer;
    private string _StatusBuffer;

    public void AddFriend(string nickname)
    {
        _NicknameBuffer = nickname;
        _StatusBuffer = "Online";
        AwakeFriend();
    }

    private void AwakeFriend()
    {
        NickName = gameObject.transform.Find("NickNameText").GetComponent<Text>();
        Status = gameObject.transform.Find("StatusText").GetComponent<Text>();
        CreateFriend();
    }

    private void CreateFriend()
    {
        NickName.text = _NicknameBuffer;
        Status.text = _StatusBuffer;
    }
}
