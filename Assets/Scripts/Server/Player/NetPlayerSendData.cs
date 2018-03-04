using UnityEngine;

public class NetPlayerSendData : MonoBehaviour
{
    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CGlChatMsg);
        buffer.WriteString(message);
        NetPlayerTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}

