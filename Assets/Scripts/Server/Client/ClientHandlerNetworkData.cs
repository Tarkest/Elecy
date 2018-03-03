﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClientHandlerNetworkData : MonoBehaviour
{

    private delegate void Packet_(byte[] data);

    private static Dictionary<int, Packet_> _Packets;

    private void Awake()
    {
        InitializeNetworkPackages();
    }

    public void InitializeNetworkPackages()
    {
        Debug.Log("Initialize Network Packages");
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)ServerPackets.SConnectionOK, HandleConnectionOK },
            {(int)ServerPackets.SRegisterOK, HandleRegisterOK },
            {(int)ServerPackets.SLoginOK, HandleLoginOK },
            {(int)ServerPackets.SAlert, HandleServerAlert },
            {(int)ServerPackets.SGlChatMsg, HandleGlobalChatMessage }
        };
    }

    public static void HandleNetworkInformation(byte[] data)
    {
        int packetNum;
        PacketBuffer buffer = new PacketBuffer();
        Packet_ Packet;
        buffer.WriteBytes(data);
        packetNum = buffer.ReadInteger();
        buffer.Dispose();
        if (_Packets.TryGetValue(packetNum, out Packet))
        {
            Packet.Invoke(data);
        }
    }

    private static void HandleConnectionOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        EntranceController.serverInfo = msg;

        ClientSendData.ConnectionComplite();
    }

    private static void HandleRegisterOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string rgstOk = buffer.ReadString();
        buffer.Dispose();
        EntranceController.serverInfo = rgstOk;

    }

    private static void HandleLoginOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();
        int[][] accountdata = new int[2][];
        int[] levels = new int[5];
        int[] ranks = new int[5];
        for (int leveli = 0; leveli < 5; leveli++)
            levels[leveli] = buffer.ReadInteger();
        for (int ranki = 0; ranki < 5; ranki++)
            ranks[ranki] = buffer.ReadInteger();
        accountdata[0] = levels;
        accountdata[1] = ranks;
        buffer.Dispose();
        string msg = "You Logged On." + nickname;
        for(int i1 = 0; i1 < 2; i1++)
        {
            for(int i2 = 0; i2 < 5; i2++)
            {
                msg += accountdata[i1][i2].ToString();
            }
        }
        EntranceController.serverInfo = msg;
        ClientTCP.ClientLogin();
    }

    private static void HandleServerAlert(byte[] data)
    {
        Debug.Log("Alert");
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        EntranceController.serverInfo = msg;
    }

    public static void HandleGlobalChatMessage(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string Nickname = buffer.ReadString();
        string Message = buffer.ReadString();
        buffer.Dispose();
        GlobalChatController.RecieveMessage(Nickname, Message);
    }
}
