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
            {(int)ServerPackets.SAlert, HandleServerAlert },

            {(int)ServerPackets.SRegisterOK, HandleRegisterOK },
            {(int)ServerPackets.SLoginOK, HandleLoginOK },

            {(int)ServerPackets.SGlChatMsg, HandleGlobalChatMessage },
            {(int)ServerPackets.SQueueStarted, HandleQueueStarted },
            {(int)ServerPackets.SMatchFound, HandleMatchFound },
            {(int)ServerPackets.SBuildInfo, HandleBuild },
            {(int)ServerPackets.SBuildSaved, HandleBuildSaved },

            {(int)ServerPackets.SLoadStarted, HandleLoadStarted },
            {(int)ServerPackets.SRockSpawn, HandleRockSpawn },
            {(int)ServerPackets.STreeSpawn, HandleTreeSpawn },
            {(int)ServerPackets.SEnemyLoadProgress, HandleEnemyLoadProgress },
            {(int)ServerPackets.SRoomStart, HandleRoomStart },
            {(int)ServerPackets.SMatchResult, HandleMatchResult },
            {(int)ServerPackets.SPlayerLogOut, HandlePlayerLogOut },
            {(int)ServerPackets.SSpellLoad, HandleSpellLoad }
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
        int mapIndex = buffer.ReadInteger();
        buffer.Dispose();
        Network.InBattle(mapIndex);
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
        int[] SkillArray = new int[buffer.ReadInteger()];
        for (int i = 0; i < SkillArray.Length; i++)
        {
            SkillArray[i] = buffer.ReadInteger();
        }
        buffer.Dispose();
        ArmoryController.SetSkills(SkillArray);
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
    public static void HandleLoadStarted(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        BattleLoader.SpanwPlayers(
                                  buffer.ReadString(),
                                  buffer.ReadString(),
                                  new float[][] { buffer.ReadVector3(), buffer.ReadVector3() },
                                  new float[][] { buffer.ReadQuternion(), buffer.ReadQuternion() }
                                  );
        buffer.Dispose();

    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int rockCount;
    ///                     for(rockCount)
    ///                     {
    ///                         int Index;
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
        float[][] rocksPos = new float[numberOfRocks][];
        float[][] rocksRot = new float[numberOfRocks][];
        for (int i = 0; i < numberOfRocks; i++)
        {
            rocksIndexes[i] = buffer.ReadInteger();
            rocksPos[i] = buffer.ReadVector3();
            rocksRot[i] = buffer.ReadQuternion();
        }
        buffer.Dispose();
        BattleLoader.LoadRocks(numberOfRocks, rocksIndexes, rocksPos, rocksRot);
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int treeCount;
    ///                     for(treeCount)
    ///                     {
    ///                         int Index;
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
        int[] _spellsIndexes = new int[_spellArrayLenght];
        for (int i = 0; i < _spellArrayLenght; i++)
        {
            _spellsIndexes[i] = buffer.ReadInteger();
        }
        BattleLoader.LoadSpells(_spellsIndexes);
        RoomUDP.Create();
        RoomUDP.BeginReceive();
        RoomUDPSendData.SendConnectionOk();
        buffer.Dispose();
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

