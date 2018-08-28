using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUDPHandleNetworkInformation : MonoBehaviour {

    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> _Packets;

    public static void InitializeNetworkPackages()
    {
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)UDPServerPackets.USConnectionOK, HandleConnectionOk},
            {(int)UDPServerPackets.USTransformUpdate, HandleMovePosition },
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
        byte type = buffer.ReadByte();
        int index = buffer.ReadInteger();
        int updateIndex = buffer.ReadInteger();
        float[] pos = new float[] { buffer.ReadFloat(), buffer.ReadFloat() };
        buffer.Dispose();
        switch(type)
        {
            case 1:
                Network.currentManager.Players[index].CheckPosition(updateIndex, pos);
                break;

            case 4:
                Network.currentManager.dynamicPropList.Get(index).CheckPosition(updateIndex, pos);
                break;
        }
    }
}
