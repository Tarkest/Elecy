
using UnityEngine;

public class ClientSendData : MonoBehaviour {

    public static void ConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectcomplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
        EntranceController.serverInfo = "You are connected to the server.";
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

    public static void SendClose()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CClose);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CGlChatMsg);
        buffer.WriteString(message);
        Debug.Log(message + " THIS IS MESSAGEE!!!!");
        NetPlayerTCP.SendData(buffer.ToArray());
        Debug.Log("Message sended");
        buffer.Dispose();

    }
}
