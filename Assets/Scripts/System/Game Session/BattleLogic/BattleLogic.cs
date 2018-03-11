using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BattleLogic : MonoBehaviour {

    public static Timer Timer;

    public static void BeginBattle()
    {
        Timer = new Timer(SendInfo, null, 0, 1000 / NetworkConstants.UPDATE_RATE);
        PlayerMovement.StartBattle();
    }

    private static void SendInfo(object o)
    {
        RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
    }
}
