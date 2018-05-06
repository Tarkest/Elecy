using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProp : MonoBehaviour {

    public int _index;
    public int _hp;
    public int[] _effects;

    public void SendInfo()
    {
        RoomSendData.SendStaticObjectInfo(_index, _hp);
    }
}
