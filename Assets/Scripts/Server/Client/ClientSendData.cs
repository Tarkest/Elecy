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

    public static void SendLogin()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CLoginTry);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Name);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
        EntranceController.serverInfo = "Loggin In...";
    }

    public static void SendRegister()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CRegisterTry);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Name);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Password);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Nickname);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
        EntranceController.serverInfo = "Creating new account...";
    }

    public static void SendClose(int pIndex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CClose);
        buffer.WriteInteger(pIndex);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendExit()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)SystemPackets.SysExit);
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
