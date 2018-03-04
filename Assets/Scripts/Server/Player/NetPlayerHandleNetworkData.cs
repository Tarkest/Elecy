using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public static void HandleConnectionOK(byte[] data)
    {

    }

    public static void HandleServerAlert(byte[] data)
    {

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
