using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BattleLogic : MonoBehaviour {

    public static Timer Timer;

    public static void BeginBattle()
    {
        PlayerMovement1.StartBattle();
        Timer = new Timer(SendInfo, null, 0, 1000 / NetworkConstants.UPDATE_RATE);
    }

    private static void SendInfo(object o)
    {
        RoomSendData.SendTransform(ObjectManager.playerPos, ObjectManager.playerRot);
    }
}
