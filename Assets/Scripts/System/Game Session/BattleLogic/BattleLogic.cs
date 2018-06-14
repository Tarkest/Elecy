using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BattleLogic : MonoBehaviour {

    public static Timer MainTimer;
    public static Timer StaticTimer;

    public static void BeginBattle()
    {
        ObjectManager.playerStats.battleIsOn = true;
        RoomController.battleIsOn = true;
        MainTimer = new Timer(SendInfo, null, 0, 1000 / NetworkConstants.UPDATE_RATE);
        StaticTimer = new Timer(SendStaticInfo, null, 0, 1000 / (NetworkConstants.UPDATE_RATE / 5));
    }

    private static void SendInfo(object o)
    {
        ObjectManager.SendPlayerUpdate();
        ObjectManager.SendDynamicObjectUpdate();
    }

    private static void SendStaticInfo(object o)
    {
        ObjectManager.SendStaticObjectUpdate();
    }

    public static void EndBattle(string Nickname)
    {
        try
        {
            MainTimer.Dispose();
            StaticTimer.Dispose();
        } catch { return; }
        ObjectManager.playerStats.battleIsOn = false;
        RoomController.battleIsOn = false;
        RoomController.ViewStatisticScreen(Nickname);
    }
}
