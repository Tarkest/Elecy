using System;
using System.Collections.Generic;
using UnityEngine;

public class NetPlayerHandleNetworkData
{
    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> _Packets;

    public static void InitializeNetworkPackages()
    {
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)ServerPackets.SConnectionOK, HandleConnectionOK },
            {(int)ServerPackets.SAlert, HandleServerAlert },
            {(int)ServerPackets.SGlChatMsg, HandleGlobalChatMessage },
            {(int)ServerPackets.SQueueStarted, HandleQueueStarted },
            {(int)ServerPackets.SQueueContinue, HandleQueueContinue },
            {(int)ServerPackets.SMatchFound, HandleMatchFound },
            {(int)ServerPackets.SNetPlayerLogOut, HandleNetPlayerLogOut },
            {(int)ServerPackets.SBuildInfo, HandleBuild },
            {(int)ServerPackets.SBuildSaved, HandleBuildSaved }
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

    public static void HandleConnectionOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();
        GlobalChatController.RecieveMessage("Server", msg);
    }

    public static void HandleServerAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string alert = buffer.ReadString();
        GlobalChatController.RecieveMessage("Server", alert);
    }

    public static void HandleGlobalChatMessage(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();
        string msg = buffer.ReadString();
        buffer.Dispose();
        GlobalChatController.RecieveMessage(nickname, msg);
    }

    public static void HandleQueueStarted(byte[] data)
    {
        MainLobbyController.isSearching = true;
        NetPlayerSendData.SendSearching();
    }

    public static void HandleQueueContinue(byte[] data)
    {
        NetPlayerSendData.SendSearching();
    }

    public static void HandleMatchFound(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int roomindex = buffer.ReadInteger();
        int mapIndex = buffer.ReadInteger();
        buffer.Dispose();
        NetPlayerTCP.Stop();
        Network.InBattle(roomindex, mapIndex);
    }

    public static void HandleNetPlayerLogOut(byte[] data)
    {
        NetPlayerTCP.Stop("");
        //ClientTCP.BeginReceive();
        Network.LogOut();
    }

    public static void HandleBuild(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int[] IgnisArray = new int[buffer.ReadInteger()];
        int[] TerraArray = new int[buffer.ReadInteger()];
        int[] CaeliArray = new int[buffer.ReadInteger()];
        int[] AquaArray = new int[buffer.ReadInteger()];
        for(int i= 0; i == IgnisArray.Length; i++)
        {
            IgnisArray[i] = buffer.ReadInteger();
        }
        for (int i = 0; i == TerraArray.Length; i++)
        {
            TerraArray[i] = buffer.ReadInteger();
        }
        for (int i = 0; i == CaeliArray.Length; i++)
        {
            CaeliArray[i] = buffer.ReadInteger();
        }
        for (int i = 0; i == AquaArray.Length; i++)
        {
            AquaArray[i] = buffer.ReadInteger();
        }
        buffer.Dispose();
        hgtygt
    }
}
