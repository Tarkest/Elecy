﻿using UnityEngine;
using System.Collections.Generic;

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
            {(int)ServerPackets.SAlert, HandleServerAlert }
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

        Debug.Log(msg);

        ClientSendData.ConnectionComplite();
    }

    private static void HandleRegisterOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();


        //EntranceController.TextInfo(3);
    }

    private static void HandleLoginOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();

        //EntranceController.TextInfo(5);
    }

    private static void HandleServerAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();

        Debug.Log(msg);
    }
}
