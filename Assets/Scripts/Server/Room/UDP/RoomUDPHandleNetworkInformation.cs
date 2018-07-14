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
            {2, HandleTest }
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
        DeveloperScreenController.AddInfo("UDP Connection...OK", 1);
    }


    public static void HandleTest(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        int packet = buffer.ReadInteger();
        buffer.Dispose();
        RoomController.AddPacket(packet);
    }
}
