﻿using System.Collections;
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
            {(int)ServerPackets.SRockSpawn, HandleRockSpawn },
            {(int)ServerPackets.STreeSpawn, HandleTreeSpawn },
            {(int)ServerPackets.SEnemyLoadProgress, HandleEnemyLoadProgress },
            {(int)ServerPackets.SRoomStart, HandleRoomStart },
            {(int)ServerPackets.STransform, HandleEnemyTransform },
            {(int)ServerPackets.SInstantiate, HandleServerInstantiate }
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

    public static void HandleRockSpawn(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int numberOfRocks = buffer.ReadInteger();
        int[] rocksIndexes = new int[numberOfRocks];
        float[][] rocksPos = new float[numberOfRocks][];
        float[][] rocksRot = new float[numberOfRocks][];
        for(int i = 0; i < numberOfRocks; i++)
        {
            rocksIndexes[i] = buffer.ReadInteger();
            rocksPos[i] = buffer.ReadVector3();
            rocksRot[i] = buffer.ReadQuternion();
        }
        buffer.Dispose();
        BattleLoader.LoadRocks(numberOfRocks, rocksIndexes, rocksPos, rocksRot);
    }

    public static void HandleTreeSpawn(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int numberoftrees = buffer.ReadInteger();
        int[] treesindexes = new int[numberoftrees];
        float[][] treespos = new float[numberoftrees][];
        float[][] treesrot = new float[numberoftrees][];
        for (int i = 0; i < numberoftrees; i++)
        {
            treesindexes[i] = buffer.ReadInteger();
            treespos[i] = buffer.ReadVector3();
            treesrot[i] = buffer.ReadQuternion();
        }
        buffer.Dispose();
        BattleLoader.LoadTrees(numberoftrees, treesindexes, treespos, treesrot);
    }

    public static void HandleEnemyLoadProgress(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        float progress = buffer.ReadFloat();
        buffer.Dispose();
        BattleLoader.EnemyProgressChange(progress);
    }

    public static void HandleRoomStart(byte[] data)
    {
        BattleLoader.StartBattle();
    }

    public static void HandleServerInstantiate(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        //get info about object for instatiate? add it to array and start to send observw info 
    }

    public static void HandleEnemyTransform(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        float[] enemyTransform = buffer.ReadVector3();
        float[] enemyRotation = buffer.ReadQuternion();
        buffer.Dispose();

        GlobalObjects.enemyMovement.SetTransform(enemyTransform, enemyRotation);
    }
}
