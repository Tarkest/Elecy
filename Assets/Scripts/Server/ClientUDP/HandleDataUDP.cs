using System;
using System.Collections.Generic;
using UnityEngine;

public class HandleDataUDP : MonoBehaviour {

    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> _Packets;

    public static void InitializeNetworkPackages()
    {
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)UDPServerPackets.USConnectionOK, HandleConnectionOk},
            {(int)UDPServerPackets.USPositionUpdate, HandleMovePosition },
            {(int)UDPServerPackets.USRotationUpdate, HandleRotation },
            {(int)UDPServerPackets.USHealthUpdate, HandleHealth },
            {(int)UDPServerPackets.USSynergyUpdate, HandleSynergy }
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

    public static void HandleConnectionOk(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        buffer.Dispose();
        BattleLoader.LoadComplite();
    }

    public static void HandleMovePosition(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        ObjectType type = (ObjectType)buffer.ReadInteger();
        int index = buffer.ReadInteger();
        int updateIndex = buffer.ReadInteger();
        Vector3 pos = new Vector3(buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat());
        buffer.Dispose();
        switch(type)
        {
            case ObjectType.player:
                Network.currentManager.Players[index].positionUpdate.Handle(updateIndex, pos);
                break;

            case ObjectType.spell:
                try
                {
                    Network.currentManager.dynamicPropList.Get(index).positionUpdate.Handle(updateIndex, pos);
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex is IndexOutOfRangeException)
                        return;
                    throw;
                }

                break;
        }
    }

    private static void HandleRotation(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        ObjectType type = (ObjectType)buffer.ReadInteger();
        int index = buffer.ReadInteger();
        int updateIndex = buffer.ReadInteger();
        Quaternion rot = new Quaternion(buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat());
        buffer.Dispose();
        switch(type)
        {
            case ObjectType.player:
                if(!Network.currentManager.Players[index].isMain)
                    Network.currentManager.Players[index].rotationUpdate.Handle(updateIndex, rot);
                break;

            case ObjectType.spell:
                try
                {
                    Network.currentManager.dynamicPropList.Get(index);
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex is IndexOutOfRangeException)
                        return;
                    throw;
                }
                break;
        }
    }

    private static void HandleHealth(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        ObjectType type = (ObjectType)buffer.ReadInteger();
        int index = buffer.ReadInteger();
        int updateIndex = buffer.ReadInteger();
        int health = buffer.ReadInteger();
        buffer.Dispose();
        switch(type)
        {
            case ObjectType.player:
                Network.currentManager.Players[index].hpUpdate.Handle(updateIndex, health);
                break;

            case ObjectType.spell:
                try
                {
                    Network.currentManager.dynamicPropList.Get(index).hpUpdate.Handle(updateIndex, health);
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex is IndexOutOfRangeException)
                        return;
                    throw;
                }
                break;
        }
    }

    private static void HandleSynergy(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        ObjectType type = (ObjectType)buffer.ReadInteger();
        int index = buffer.ReadInteger();
        int updateIndex = buffer.ReadInteger();
        int synergy = buffer.ReadInteger();
        buffer.Dispose();
        switch (type)
        {
            case ObjectType.player:
                Network.currentManager.Players[index].synergyUpdate.Handle(updateIndex, synergy);
                break;
        }
    }
}
