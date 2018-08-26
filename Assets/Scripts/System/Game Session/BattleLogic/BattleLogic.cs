using UnityEngine;
using System.Timers;
using System.Threading;

public class BattleLogic : MonoBehaviour {

    private static System.Timers.Timer battleTimer;

    public static void BeginBattle()
    {
        battleTimer = new System.Timers.Timer(GSC.timerTick);
        battleTimer.Elapsed += OnBattleTimerEvent;
        battleTimer.AutoReset = true;
        Thread.Sleep(5000);
        StartTimer();
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
        MainThread.executeInUpdate(() => { ObjectManager.players[ObjectManager.playerMovement].Move(); });
        MainThread.executeInUpdate(() => { Network.currentManager.UpdatePrefabs(); });
    }

    public static void EndBattle(string Nickname)
    {
        battleTimer.Dispose();
        RoomController.battleIsOn = false;
        RoomController.ViewStatisticScreen(Nickname);
    }
}
