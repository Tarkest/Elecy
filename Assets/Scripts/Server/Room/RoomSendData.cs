using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSendData : MonoBehaviour {

    public static void SendConnectionOk(int roomindex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        buffer.WriteInteger(roomindex);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendLoadComplite(Vector3 playertransform, Quaternion playerrotation)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadComplite);
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
