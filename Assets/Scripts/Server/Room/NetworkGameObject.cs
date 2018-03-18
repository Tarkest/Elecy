using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameObject : MonoBehaviour {

    public int thisIndex;
    public int serverHp;
    public Vector3 serverPos;
    public Quaternion serverRot;

    public void SetTransform(Vector3 transform, Quaternion rotation)
    {
        serverPos = transform;
        serverRot = rotation;
    }
    
    public void SetHp(int hp)
    {
        serverHp = hp;
    }

    public void SetIndex(int index)
    {
        thisIndex = index;
    }

    public Vector3 GetPosition()
    {
        return serverPos;
    }

    public Quaternion GetRotation()
    {
        return serverRot;
    }

    public int GetHp()
    {
        return serverHp;
    }

    public int GetIndex()
    {
        return thisIndex;
    }
}
