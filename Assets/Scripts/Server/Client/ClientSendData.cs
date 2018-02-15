
using UnityEngine;

public class ClientSendData : MonoBehaviour {

    public static void ConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectcomplite);
        buffer.WriteString("Connection of client succesfull.");
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();

        //EntranceController.TextInfo(1);
    }

    public static void SendLogin()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CLoginTry);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Name);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();

        //EntranceController.TextInfo(4);
    }

    public static void SendRegister()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CRegisterTry);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Name);
        buffer.WriteString(GameObject.Find("EntranceController").GetComponent<EntranceController>().Password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();

        //EntranceController.TextInfo(2);
    }
}
