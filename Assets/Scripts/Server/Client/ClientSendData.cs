using UnityEngine;

public class ClientSendData
{
    public static void SendConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendLogin(string name, string password)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CLoginTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
        EntranceController.GetInProcess("Loggin in...");
    }

    public static void SendRegister(string name, string password, string nickname)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CRegisterTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        buffer.WriteString(nickname);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
        EntranceController.GetInProcess("Registration...");
    }

    public static void SendClose(int pIndex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CClose);
        buffer.WriteInteger(pIndex);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendReconnect()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CReconnectComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
