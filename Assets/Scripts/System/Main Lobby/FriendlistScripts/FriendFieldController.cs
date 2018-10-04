using UnityEngine;
using UnityEngine.UI;

public class FriendFieldController : MonoBehaviour {

    private Image Avatar;
    private Text NickName;
    private Text Status;

    private string _NicknameBuffer;
    private string _StatusBuffer;
    private string[] _statuses = new string[5] { "","In Main Lobby", "Searching For Match", "Playing", "End Playing" };

    public void AddFriend(string nickname, int status)
    {
        _NicknameBuffer = nickname;
        _StatusBuffer = _statuses[status];
        AwakeFriend();
    }

    private void AwakeFriend()
    {
        Avatar = gameObject.transform.Find("Avatar").GetComponent<Image>();
        NickName = gameObject.transform.Find("Text").transform.Find("Nickname").GetComponent<Text>();
        Status = gameObject.transform.Find("Text").transform.Find("Status").GetComponent<Text>();
        CreateFriend();
    }

    private void CreateFriend()
    {
        NickName.text = _NicknameBuffer;
        Status.text = _StatusBuffer;
    }

    public void ChangeStatus(int status)
    {
        MainThread.executeInUpdate(() => Status.text = _statuses[status]);
    }
}
