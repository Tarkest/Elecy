﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SendDataTCP
{

    #region Entrance

    /// <summary>
    ///             Buffer:
    ///                     int PakcetNum;
    /// </summary>
    public static void SendConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string username;
    ///                     string password;
    /// </summary>
    public static void SendLogin(string name, string password)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CLoginTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string username;
    ///                     string password;
    ///                     string nickname;
    /// </summary>
    public static void SendRegister(string name, string password, string nickname)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CRegisterTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        buffer.WriteString(nickname);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    #endregion

    #region MainLobby

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendLoginComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PConnectionComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string message;
    /// </summary>
    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGlChatMsg);
        buffer.WriteString(message);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int matchType;
    ///                     string race;
    /// </summary>
    public static void SendQueueStart(int MatchType, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStart);
        buffer.WriteInteger(MatchType);
        buffer.WriteString(race);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendQueueStop()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStop);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string race;
    /// </summary>
    public static void SendGetSkillBuild(string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGetSkillsBuild);
        buffer.WriteString(race);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string race;
    ///                     int spellCount;
    ///                     int[spellCount] spellIndex;
    /// </summary>
    public static void SendSaveSkillBuild(int[] Build, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PSaveSkillsBuild);
        buffer.WriteString(race);
        buffer.WriteInteger(Build.Length);
        for (int i = 0; i < Build.Length; i++)
        {
            buffer.WriteInteger(Build[i]);
        }
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    #endregion

    #region GameRoom

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendEnterRoom()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendBeginLoading(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetPlayers);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    ///                     int RocksTypeCount;
    ///                     bool BigExist;
    ///                     bool MiddleExist;
    ///                     bool SmallExist;
    /// </summary>
    public static void SendSpawnRocks(float LoadProgress, int RocksTypeCount, bool BigExist, bool MiddleExist, bool SmallExist)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetRocks);
        buffer.WriteFloat(LoadProgress);
        buffer.WriteInteger(RocksTypeCount);
        buffer.WriteBoolean(BigExist);
        buffer.WriteBoolean(MiddleExist);
        buffer.WriteBoolean(SmallExist);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    ///                     int TreesTypeCount;
    ///                     bool BigExist;
    ///                     bool MiddleExist;
    ///                     bool SmallExist;
    /// </summary>
    public static void SendSpawnTrees(float LoadProgress, int TreesTypeCount, bool BigExist, bool MiddleExist, bool SmallExist)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetTrees);
        buffer.WriteFloat(LoadProgress);
        buffer.WriteInteger(TreesTypeCount);
        buffer.WriteBoolean(BigExist);
        buffer.WriteBoolean(MiddleExist);
        buffer.WriteBoolean(SmallExist);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendGetSpells(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetSpells);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendLoadComplite(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadComplite);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendSurrender()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RSurrender);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendRoomLeave()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RRoomLeave);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    #endregion

}
