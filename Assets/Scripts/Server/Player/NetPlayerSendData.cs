using UnityEngine;

public class NetPlayerSendData
{

    public static void SendConnectionComplpite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PConnectionComplite);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGlChatMsg);
        buffer.WriteString(message);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendQueueStart(int MatchType)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStart);
        buffer.WriteInteger(MatchType);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendSearching(float searchTimeCounter)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PSearch);
        buffer.WriteFloat(searchTimeCounter);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendQueueStop()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStop);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendBeginMatchLoad(int roomindex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PBeginMatchLoad);
        buffer.WriteInteger(roomindex);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendAlert(string alert)
    {

    }
}

