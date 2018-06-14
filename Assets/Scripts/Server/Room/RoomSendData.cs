using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSendData : MonoBehaviour {

    public static void SendConnectionOk(int roomindex)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPlayerSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RPlayerSpawned);
        buffer.WriteFloat(0.33f);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendRocksSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RRockSpawned);
        buffer.WriteFloat(0.66f);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTreesSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RTreeSpawned);
        buffer.WriteFloat(0f);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendLoadComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadComplite);
        buffer.WriteFloat(1f);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendInstatiate(int ID, Vector3 Position, Quaternion Rotation)
    {
        float[] objectPos = new float[] { Position.x, Position.y, Position.z };
        float[] objectRot = new float[] { Rotation.x, Rotation.y, Rotation.z, Rotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RInstantiate);
        buffer.WriteInteger(ID);
        buffer.WriteVector3(objectPos);
        buffer.WriteQuaternion(objectRot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendDestroy(int array, int index)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RDestroy);
        buffer.WriteInteger(array);
        buffer.WriteInteger(index);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPlayerUpdate(Vector3 playerTransform, Quaternion playerRotation, int hp, int synergy, List<int> effects)
    {
        float[] playerPos = new float[] { playerTransform.x, playerTransform.y, playerTransform.z };
        float[] playerRot = new float[] { playerRotation.x, playerRotation.y, playerRotation.z, playerRotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RPlayerUpdate);
        buffer.WriteVector3(playerPos);
        buffer.WriteQuaternion(playerRot);
        buffer.WriteInteger(hp);
        buffer.WriteInteger(synergy);
        buffer.WriteInteger(effects.Count);
        for(int i = 0; i <= effects.Count - 1; i++)
        {
            buffer.WriteInteger(effects[i]);
        }
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendStaticObjectInfo(int index, int hp, List<int> effects)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RStaticObjUpdate);
        buffer.WriteInteger(index);
        buffer.WriteInteger(hp);
        buffer.WriteInteger(effects.Count);
        for(int i = 0; i <= effects.Count - 1; i++)
        {
            buffer.WriteInteger(effects[i]);
        }
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendStaticObjectInfo(int index, int hp, List<int> effects, Vector3 pos, Quaternion rot)
    {
        float[] Pos = new float[] { pos.x, pos.y, pos.z };
        float[] Rot = new float[] { rot.x, rot.y, rot.z, rot.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RStaticObjUpdate);
        buffer.WriteInteger(index);
        buffer.WriteInteger(hp);
        buffer.WriteInteger(effects.Count);
        for (int i = 0; i <= effects.Count - 1; i++)
        {
            buffer.WriteInteger(effects[i]);
        }
        buffer.WriteVector3(Pos);
        buffer.WriteQuaternion(Rot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendDynamicObjectInfo(int index, Vector3 position, Quaternion rotation)
    {
        float[] objectPos = new float[] { position.x, position.y, position.z };
        float[] objectRot = new float[] { rotation.x, rotation.y, rotation.z, rotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RDynamicObjUpdate);
        buffer.WriteInteger(index);
        buffer.WriteVector3(objectPos);
        buffer.WriteQuaternion(objectRot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendSurrender()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RSurrender);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendRoomLeave()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RRoomLeave);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
