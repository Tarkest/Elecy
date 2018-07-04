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

    public static void SendQueueStart(int MatchType, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStart);
        buffer.WriteInteger(MatchType);
        buffer.WriteString(race);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendSearching()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PSearch);
        buffer.WriteFloat(0.5f); // WriteFloat
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

    public static void SendStopPlayer()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PStopPlayer);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendAlert(string alert)
    {

    }

    public static void SendGetSkillBuild(string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGetSkillsBuild);
        buffer.WriteString(race);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendSaveSkillBuild(int[] Build, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PSaveSkillsBuild);
        buffer.WriteString(race);
        buffer.WriteInteger(Build.Length);
        for(int i = 0; i < Build.Length; i++)
        {
            buffer.WriteInteger(Build[i]);
        }
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}

