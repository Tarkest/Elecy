using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClientHandlerNetworkData : MonoBehaviour
{

    private delegate void Packet_(byte[] data);

    private static Dictionary<int, Packet_> _Packets;

    [System.NonSerialized]
    public static bool scenechange = false;

    private void Awake()
    {
        InitializeNetworkPackages();
    }

    private void Update()
    {
        if(scenechange)
        {
            LoadScene();
        }
    }

    public void InitializeNetworkPackages()
    {
        Debug.Log("Initialize Network Packages");
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)ServerPackets.SConnectionOK, HandleConnectionOK },
            {(int)ServerPackets.SRegisterOK, HandleRegisterOK },
            {(int)ServerPackets.SLoginOK, HandleLoginOK },
            {(int)ServerPackets.SAlert, HandleServerAlert }
        };
    }

    public static void HandleNetworkInformation(byte[] data)
    {
        Debug.Log("I'm in handler");
        int packetNum;
        string username = ""; //////
        PacketBuffer buffer = new PacketBuffer();
        Packet_ Packet;
        buffer.WriteBytes(data);
        packetNum = buffer.ReadInteger();
        if(packetNum == 13) //////
        {
            username = buffer.ReadString();
            Debug.Log(username);
        } else
        {
            Debug.Log(packetNum + " isn't 13");
        }
        buffer = null;
        Debug.Log("I'm in handler2 || " + packetNum + " || " + username);
        if (_Packets.TryGetValue(packetNum, out Packet))
        {
            Debug.Log("I'm in handler3");
            Packet.Invoke(data);
        }
    }

    private static void HandleConnectionOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        EntranceController.serverInfo = msg;

        ClientSendData.ConnectionComplite();
    }

    private static void HandleRegisterOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string rgstOk = buffer.ReadString();
        buffer.Dispose();

        EntranceController.serverInfo = rgstOk;

    }

    private static void HandleLoginOK(byte[] data)
    {
        Debug.Log("I'm in logOk");
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();

        EntranceController.serverInfo = "You Logged On.";
        scenechange = true;
    }

    private static void HandleServerAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        EntranceController.serverInfo = msg;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
