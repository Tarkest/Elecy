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
}
