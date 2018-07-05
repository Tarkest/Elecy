using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BattleLogic : MonoBehaviour {

    public static Timer Timer;

    public static void BeginBattle()
    {
        //ObjectManager.playerStats.battleIsOn = true;
        //RoomController.battleIsOn = true;
        //Timer = new Timer(SendInfo, null, 0, 1000 / NetworkConstants.UPDATE_RATE);
    }

    private static void SendInfo(object o)
    {
        //RoomSendData.SendTransform(ObjectManager.playerPos, ObjectManager.playerRot);
    }

    public static void EndBattle(string Nickname)
    {
        //Timer.Dispose();
        ObjectManager.playerStats.battleIsOn = false;
        RoomController.battleIsOn = false;
        RoomController.ViewStatisticScreen(Nickname);
    }
}
