using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandleNetworkInformation : MonoBehaviour {

    private delegate void Packet_(byte[] data);
    private static Dictionary<int, Packet_> _Packets;

    public static void InitializeNetworkPackages()
    {
        _Packets = new Dictionary<int, Packet_>
        {
            {(int)ServerPackets.SLoadStarted, HandleLoadStarted },
            {(int)ServerPackets.SRoomStart, HandleRoomStart },
            {(int)ServerPackets.STransform, HandleEnemyTransform }
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

    public static void HandleLoadStarted(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string Nickname1 = buffer.ReadString();
        string Nickname2 = buffer.ReadString();
        buffer.Dispose();
        BattleLoader.SpanwPlayers(Nickname1, Nickname2);
    }

    public static void HandleRoomStart(byte[] data)
    {
        BattleLoader.StartBattle();
    }

    public static void HandleEnemyTransform(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        Vector3 enemyTransform = new Vector3(buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat());
        Quaternion enemyRotation = new Quaternion(buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat());
        buffer.Dispose();

        GlobalObjects.enemyMovement.SetTransform(enemyTransform, enemyRotation);
        RoomSendData.SendTransform(GlobalObjects.playerPos, GlobalObjects.playerRot);
    }
}
