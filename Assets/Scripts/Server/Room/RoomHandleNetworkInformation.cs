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
            {(int)ServerPackets.SRockSpawn, HandleRockSpawn },
            {(int)ServerPackets.STreeSpawn, HandleTreeSpawn },
            {(int)ServerPackets.SEnemyLoadProgress, HandleEnemyLoadProgress },
            {(int)ServerPackets.SRoomStart, HandleRoomStart },
            //{(int)ServerPackets.STransform, HandleEnemyTransform },
            {(int)ServerPackets.SInstantiate, HandleServerInstantiate },
            {(int)ServerPackets.SMatchResult, HandleMatchResult },
            {(int)ServerPackets.SPlayerLogOut, HandlePlayerLogOut },
            {(int)ServerPackets.SSpellLoad, HandleSpellLoad }
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
        float[][] spawnPos = new float[2][];
        float[][] spawnRot = new float[2][];
        spawnPos[0] = buffer.ReadVector3();
        spawnPos[1] = buffer.ReadVector3();
        spawnRot[0] = buffer.ReadQuternion();
        spawnRot[1] = buffer.ReadQuternion();
        buffer.Dispose();
        BattleLoader.SpanwPlayers(Nickname1, Nickname2, spawnPos, spawnRot);
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

    public static void HandleSpellLoad(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int _spellArrayLenght = buffer.ReadInteger() + buffer.ReadInteger();
        int[] _spellsIndexes = new int[_spellArrayLenght];
        for(int i = 0; i < _spellArrayLenght; i++)
        {
            _spellsIndexes[i] = buffer.ReadInteger();
        }
        BattleLoader.LoadSpells(_spellsIndexes);
        buffer.Dispose();
    }

    public static void HandleMatchResult(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();
        buffer.Dispose();
        BattleLogic.EndBattle(nickname);
    }

    public static void HandlePlayerLogOut(byte[] data)
    {
        RoomTCP.Stop();
        Network.EndBattle();
    }
}
