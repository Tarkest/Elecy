using System.Collections.Generic;

public class ClientHandlerNetworkData
{
    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> _Packets;

    public static void InitializeNetworkPackages()
    {
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)ServerPackets.SConnectionOK, HandleConnectionOK },
            {(int)ServerPackets.SRegisterOK, HandleRegisterOK },
            {(int)ServerPackets.SLoginOK, HandleLoginOK },
            {(int)ServerPackets.SAlert, HandleServerAlert },
        };
    }

    public static void HandleNetworkInformation(byte[] data)
    {
        int packetNum;
        PacketBuffer buffer = new PacketBuffer();
        Packet_ Packet;
        buffer.WriteBytes(data);
        packetNum = buffer.ReadInteger();
        buffer.Dispose();
        if (_Packets.TryGetValue(packetNum, out Packet))
        {
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
        ClientSendData.SendConnectionComplite();
    }

    private static void HandleRegisterOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();
        EntranceController.serverInfo = msg;
    }

    private static void HandleLoginOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int playerIndex = buffer.ReadInteger();
        string nickname = buffer.ReadString();
        int[][] accountData = new int[2][];
        int[] levels = new int[5];
        int[] ranks = new int[5];
        for (int leveli = 0; leveli < 5; leveli++)
            levels[leveli] = buffer.ReadInteger();
        for (int ranki = 0; ranki < 5; ranki++)
            ranks[ranki] = buffer.ReadInteger();
        accountData[0] = levels;
        accountData[1] = ranks;
        buffer.Dispose();
        EntranceController.serverInfo = "You Logged On.";
        ClientTCP.Stop();
        Network.Login(playerIndex, nickname, accountData);
    }

    private static void HandleServerAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();
        EntranceController.serverInfo = "Alert: " + msg;
    }

    public static void HandleGlobalChatMessage(byte[] data) // In Player!
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string Nickname = buffer.ReadString();
        string Message = buffer.ReadString();
        buffer.Dispose();
        GlobalChatController.RecieveMessage(Nickname, Message);
    }
}
