using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSendData : MonoBehaviour {

    public static void SendConnectionOk(int roomindex)
    {
        Debug.Log("Send connect complite");
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        buffer.WriteInteger(roomindex);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPlayerSpawned(Vector3 playerTransform, Quaternion playerRotation)
    {
        Debug.Log("Send player spawned");
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RPlayerSpawned);
        buffer.WriteInteger(RoomTCP.Getindex());
        buffer.WriteFloat(playerTransform.x);
        buffer.WriteFloat(playerTransform.y);
        buffer.WriteFloat(playerTransform.z);
        buffer.WriteFloat(playerRotation.x);
        buffer.WriteFloat(playerRotation.y);
        buffer.WriteFloat(playerRotation.z);
        buffer.WriteFloat(playerRotation.w);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendLoadComplite()
    {
        Debug.Log("Send load complite");
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
        buffer.WriteFloat(playertransform.x);
        buffer.WriteFloat(playertransform.y);
        buffer.WriteFloat(playertransform.z);
        buffer.WriteFloat(playerrotation.x);
        buffer.WriteFloat(playerrotation.y);
        buffer.WriteFloat(playerrotation.z);
        buffer.WriteFloat(playerrotation.w);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
