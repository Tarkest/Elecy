using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSendData : MonoBehaviour {

    public static void SendConnectionOk(int roomindex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        buffer.WriteInteger(roomindex);
        float[] scale = RoomTCP.GetBattlegroundScale();
        buffer.WriteFloat(scale[0]);
        buffer.WriteFloat(scale[1]);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPlayerSpawned(Vector3 playerTransform, Quaternion playerRotation)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RPlayerSpawned);
        buffer.WriteInteger(RoomTCP.Getindex());
        buffer.WriteVector3(playerTransform);
        buffer.WriteQuaternion(playerRotation);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendRocksSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RRockSpawned);
        buffer.WriteInteger(RoomTCP.Getindex());
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTreesSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RTreesSpawned);
        buffer.WriteInteger(RoomTCP.Getindex());
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendLoadComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadComplite);
        buffer.WriteInteger(RoomTCP.Getindex());
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTransform(Vector3 playertransform, Quaternion playerrotation)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RTransform);
        buffer.WriteInteger(RoomTCP.Getindex());
        buffer.WriteVector3(playertransform);
        buffer.WriteQuaternion(playerrotation);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
