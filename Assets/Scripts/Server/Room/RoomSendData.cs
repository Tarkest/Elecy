﻿using System.Collections;
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
        buffer.WriteFloat(GlobalObjects.firstSPpos.x);
        buffer.WriteFloat(GlobalObjects.firstSPpos.y);
        buffer.WriteFloat(GlobalObjects.firstSPpos.z);
        buffer.WriteFloat(GlobalObjects.secondSPpos.x);
        buffer.WriteFloat(GlobalObjects.secondSPpos.y);
        buffer.WriteFloat(GlobalObjects.secondSPpos.z);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPlayerSpawned()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RPlayerSpawned);
        buffer.WriteInteger(RoomTCP.Getindex());
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

    public static void SendLoadProgress(int roomIndex, float loadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadProgress);
        buffer.WriteInteger(roomIndex);
        buffer.WriteFloat(loadProgress);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTransform(Vector3 playerTransform, Quaternion playerRotation)
    {
        float[] playerPos = new float[] { playerTransform.x, playerTransform.y, playerTransform.z };
        float[] playerRot = new float[] { playerRotation.x, playerRotation.y, playerRotation.z, playerRotation.w };
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RTransform);
        buffer.WriteInteger(RoomTCP.Getindex());
        buffer.WriteVector3(playerPos);
        buffer.WriteQuaternion(playerRot);
        RoomTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
