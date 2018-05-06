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

    public static void SendInstatiate(int ID, Vector3 Position, Quaternion Rotation, int InstantiateType, string objectReference)
    {
        float[] objectPos = new float[] { Position.x, Position.y, Position.z };
        float[] objectRot = new float[] { Rotation.x, Rotation.y, Rotation.z, Rotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RInstantiate);
        buffer.WriteInteger(ID);
        buffer.WriteInteger(InstantiateType);
        buffer.WriteString(objectReference);
        buffer.WriteVector3(objectPos);
        buffer.WriteQuaternion(objectRot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTransform(Vector3 playerTransform, Quaternion playerRotation)
    {
        float[] playerPos = new float[] { playerTransform.x, playerTransform.y, playerTransform.z };
        float[] playerRot = new float[] { playerRotation.x, playerRotation.y, playerRotation.z, playerRotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RTransform);
        buffer.WriteVector3(playerPos);
        buffer.WriteQuaternion(playerRot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendStaticObjectInfo(int index, int hp/* state,effect */)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RStaticObjUpdate);
        buffer.WriteInteger(index);
        buffer.WriteInteger(hp);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendDynamicObjectInfo(int index, Vector3 position, Quaternion rotation, string state, int hp, int damage)
    {
        float[] objectPos = new float[] { position.x, position.y, position.z };
        float[] objectRot = new float[] { rotation.x, rotation.y, rotation.z, rotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RDynamicObjUpdate);
        buffer.WriteInteger(index);
        buffer.WriteVector3(objectPos);
        buffer.WriteQuaternion(objectRot);
        buffer.WriteString(state);
        buffer.WriteInteger(hp);
        buffer.WriteInteger(damage);
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
