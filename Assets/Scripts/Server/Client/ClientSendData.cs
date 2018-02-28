
using UnityEngine;

public class ClientSendData : MonoBehaviour {

    public static void ConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectcomplite);
        buffer.WriteString("Connection of client succesfull.");
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
        Debug.Log("Send closing");
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CClose);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
