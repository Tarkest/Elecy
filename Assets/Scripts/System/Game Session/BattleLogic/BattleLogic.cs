using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class BattleLogic : MonoBehaviour {

    private static Timer battleTimer;

    public static void BeginBattle()
    {
        battleTimer = new Timer(GSC.timerTick);
        battleTimer.Elapsed += OnBattleTimerEvent;
        battleTimer.AutoReset = true;
    }

    public static void StartTimer()
    {
        battleTimer.Start();
    }

    public static void StopTimer(bool dispose = false)
    {
        if (dispose)
            battleTimer.Dispose();
        else
            battleTimer.Stop();
    }

    private static void OnBattleTimerEvent(object o, ElapsedEventArgs e)
    {
        PlayerMovement.Move();
    }

    public static void EndBattle(string Nickname)
    {
        battleTimer.Dispose();
        RoomController.battleIsOn = false;
        RoomController.ViewStatisticScreen(Nickname);
    }
}
