using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class HandleDataTCP
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
            {(int)ServerPackets.SGlChatMsg, HandleGlobalChatMessage },
            {(int)ServerPackets.SQueueStarted, HandleQueueStarted },
            {(int)ServerPackets.SMatchFound, HandleMatchFound },
            {(int)ServerPackets.SMapLoad, HandleMapLoad },
            {(int)ServerPackets.SPlayerSpawned, HandlePlayerSpawn },
            {(int)ServerPackets.SRockSpawned, HandleRockSpawn },
            {(int)ServerPackets.STreeSpawned, HandleTreeSpawn },
            {(int)ServerPackets.SSpellLoaded, HandleSpellLoad },
            {(int)ServerPackets.SRoomStart, HandleRoomStart },
            {(int)ServerPackets.SEnemyLoadProgress, HandleEnemyLoadProgress },
            {(int)ServerPackets.SPlayerLogOut, HandlePlayerLogOut },
            {(int)ServerPackets.SMatchResult, HandleMatchResult },
            {(int)ServerPackets.SBuildInfo, HandleBuild },
            {(int)ServerPackets.SBuildSaved, HandleBuildSaved },
        };
    }

    public static void HandleNetworkInformation(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        Packet_ Packet;
        buffer.WriteBytes(data);
        int packetNum = buffer.ReadInteger();
        buffer.Dispose();
        if (_Packets.TryGetValue(packetNum, out Packet))
        {
            Packet.Invoke(data);
        }
    }

    #region Route

    private static void HandleConnectionOK(byte[] data)
    {
        if (ClientTCP.clientState == ClientTCP.GameState.Entrance)
            HandleEntranceConnectionOK(data);
        else if (ClientTCP.clientState == ClientTCP.GameState.MainLobby)
            HandleMainLobbyConnectionOK(data);
    }

    private static void HandleServerAlert(byte[] data)
    {
        if (ClientTCP.clientState == ClientTCP.GameState.Entrance)
            HandleEntranceAlert(data);
        else if (ClientTCP.clientState == ClientTCP.GameState.MainLobby)
            HandleMainLobbyAlert(data);
    }

    #endregion

    #region Entrance

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    private static void HandleEntranceConnectionOK(byte[] data)
    {
        EntranceController.GetOffProcess();
        SendDataTCP.SendConnectionComplite();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    private static void HandleRegisterOK(byte[] data)
    {
        EntranceController.GetOffProcess();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string nickname;
    ///                     int[5] level;
    ///                     int[5] rank;
    /// </summary>
    private static void HandleLoginOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
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
        EntranceController.GetOffProcess();
        Network.Login(1, nickname, accountData);
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string message;
    /// </summary>
    private static void HandleEntranceAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        EntranceController.GetOffProcess();
        EntranceController.GetError(buffer.ReadString());
        buffer.Dispose();
    }

    #endregion

    #region MainLobby

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string message;
    /// </summary>
    public static void HandleMainLobbyConnectionOK(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        GlobalChatController.RecieveMessage("Server", buffer.ReadString());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string alert;
    /// </summary>
    public static void HandleMainLobbyAlert(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        GlobalChatController.RecieveMessage("Server", buffer.ReadString());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string nickname;
    ///                     string message;
    /// </summary>
    public static void HandleGlobalChatMessage(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        GlobalChatController.RecieveMessage(buffer.ReadString(), buffer.ReadString());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void HandleQueueStarted(byte[] data)
    {
        MainLobbyController.isSearching = true;
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int mapIndex;
    /// </summary>
    public static void HandleMatchFound(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        buffer.Dispose();
        Network.InBattle();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string race;
    ///                     int spellCount;
    ///                     int[spellCount] spellIndex;
    /// </summary>
    public static void HandleBuild(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string race = buffer.ReadString();
        int spellcount = buffer.ReadInteger();
        short[] _skillArray = new short[spellcount];
        short[] _skillVariation = new short[spellcount];
        for (int i = 0; i < spellcount; i++)
        {
            _skillArray[i] = buffer.ReadShort();
            _skillVariation[i] = buffer.ReadShort();
        }
        buffer.Dispose();
        ArmoryController.SetSkills(_skillArray, _skillVariation);
        MainLobbyController.GetOffProcess();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void HandleBuildSaved(byte[] data)
    {
        MainLobbyController.GetOffProcess();
    }

    #endregion

    #region GameRoom

    public static void HandleMapLoad(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        BattleLoader.LoadScene(buffer.ReadInteger());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string firstPlayerNickname;
    ///                     string secondPlayerNickname;
    ///                     float[3] firstPlayerPosition;
    ///                     float[3] secondPlayerPosition;
    ///                     float[4] firstPlayerRotation;
    ///                     float[4] secondPlayerRotation;
    /// </summary>
    public static void HandlePlayerSpawn(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string Nickname1 = buffer.ReadString();
        string Nickname2 = buffer.ReadString();
        float[][] pos = new float[][] { new float[] { buffer.ReadFloat(), 0.5f, buffer.ReadFloat() }, new float[] { buffer.ReadFloat(), 0.5f, buffer.ReadFloat() } };
        float[][] rot = new float[][] { buffer.ReadQuternion(), buffer.ReadQuternion() };
        buffer.Dispose();
        MainThread.executeInUpdate(() => { BattleLoader.SpanwPlayers(Nickname1, Nickname2, pos, rot); });
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int rockCount;
    ///                     for(rockCount)
    ///                     {
    ///                         int Index;
    ///                         int Hp;
    ///                         float[3] pos;
    ///                         float[4] rot;
    ///                     }
    ///                     
    /// </summary>
    public static void HandleRockSpawn(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int numberOfRocks = buffer.ReadInteger();
        int[] rocksIndexes = new int[numberOfRocks];
        int[] rocksHP = new int[numberOfRocks];
        float[][] rocksPos = new float[numberOfRocks][];
        float[][] rocksRot = new float[numberOfRocks][];
        for (int i = 0; i < numberOfRocks; i++)
        {
            rocksIndexes[i] = buffer.ReadInteger();
            rocksHP[i] = buffer.ReadInteger();
            rocksPos[i] = buffer.ReadVector3();
            rocksRot[i] = buffer.ReadQuternion();
        }
        buffer.Dispose();
        MainThread.executeInUpdate(() => BattleLoader.SpawnRocks(numberOfRocks, rocksIndexes, rocksHP,rocksPos, rocksRot));
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int treeCount;
    ///                     for(treeCount)
    ///                     {
    ///                         int Index;
    ///                         int Hp;
    ///                         float[3] pos;
    ///                         float[4] rot;
    ///                     }
    ///                     
    /// </summary>
    public static void HandleTreeSpawn(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int numberoftrees = buffer.ReadInteger();
        int[] treesindexes = new int[numberoftrees];
        int[] treesHP = new int[numberoftrees];
        float[][] treespos = new float[numberoftrees][];
        float[][] treesrot = new float[numberoftrees][];
        for (int i = 0; i < numberoftrees; i++)
        {
            treesindexes[i] = buffer.ReadInteger();
            treesHP[i] = buffer.ReadInteger();
            treespos[i] = buffer.ReadVector3();
            treesrot[i] = buffer.ReadQuternion();
        }
        buffer.Dispose();
        MainThread.executeInUpdate(() => BattleLoader.SpawnTrees(numberoftrees, treesindexes, treesHP, treespos, treesrot));
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void HandleEnemyLoadProgress(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        BattleLoader.EnemyProgressChange(buffer.ReadFloat());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void HandleRoomStart(byte[] data)
    {
        BattleLoader.StartBattle();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int firstPlayerSpellCount;
    ///                     int secondPlayerSpellCount;
    ///                     int[firstPlayerSpellCount] spellNumber;
    ///                     int[secondPlayerSpellCount] spellNumber;
    /// </summary>
    public static void HandleSpellLoad(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int _spellArrayLenght = buffer.ReadInteger() + buffer.ReadInteger();
        short[] _spellsIndexes = new short[_spellArrayLenght];
        short[] _spellsVariation = new short[_spellArrayLenght];
        for (int i = 0; i < _spellArrayLenght; i++)
        {
            _spellsIndexes[i] = buffer.ReadShort();
            _spellsVariation[i] = buffer.ReadShort();
        }
        buffer.Dispose();
        MainThread.executeInUpdate(() => BattleLoader.LoadSpells(_spellsIndexes, _spellsVariation));
        RoomUDP.Create();
        RoomUDP.BeginReceive();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string winnerNickname;
    /// </summary>
    public static void HandleMatchResult(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string nickname = buffer.ReadString();
        buffer.Dispose();
        BattleLogic.EndBattle(nickname);
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void HandlePlayerLogOut(byte[] data)
    {
        Network.EndBattle();
    }

    #endregion

}

