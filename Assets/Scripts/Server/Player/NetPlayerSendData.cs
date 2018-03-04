using UnityEngine;

public class NetPlayerSendData
{

    public static void SendConnectionComplpite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)PlayerPackets.PConnectionComplite);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)PlayerPackets.PGlChatMsg);
        buffer.WriteString(message);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}

