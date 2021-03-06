﻿using System;
using System.Collections.Generic;

internal class HandleDataTCP
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
            {(int)ServerPackets.SInstantiate, HandleInstantiate },
            {(int)ServerPackets.SDestroy, HandleDestroy },
            {(int)ServerPackets.SDamage, HandleDamage },
            {(int)ServerPackets.SFriendsInfo, HandleFriendsInfo },
            {(int)ServerPackets.SFriendLeave, HandleFriendLeave },
            {(int)ServerPackets.SFriendInfo, HandleFriendInfo },
            {(int)ServerPackets.SFriendChange, HandleFriendChange }
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
        string guideKey = buffer.ReadString();
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
        Network.Login(1, nickname, guideKey, accountData);
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

    public static void HandleFriendsInfo(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int _friendsCount = buffer.ReadInteger();
        string[] _nicknames = new string[_friendsCount];
        string[] _guideKeys = new string[_friendsCount];
        int[] _statuses = new int[_friendsCount];
        for (int i = 0; i < _friendsCount; i++)
        {
            _nicknames[i] = buffer.ReadString();
            _guideKeys[i] = buffer.ReadString();
            _statuses[i] = buffer.ReadInteger();
        }
        buffer.Dispose();
        MainThread.executeInUpdate(()=>FriendListController.AddFriends(_nicknames, _guideKeys, _statuses));
    }

    public static void HandleFriendInfo(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string _nickname = buffer.ReadString();
        string _guideKey = buffer.ReadString();
        int _status = buffer.ReadInteger();
        buffer.Dispose();
        MainThread.executeInUpdate(() => FriendListController.AddFriend(_nickname, _guideKey, _status));
    }

    public static void HandleFriendChange(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string _guideKey = buffer.ReadString();
        int status = buffer.ReadInteger();
        buffer.Dispose();
        FriendListController.ChangeStatus(_guideKey, status);
    }

    public static void HandleFriendLeave(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        string _guideKey = buffer.ReadString();
        buffer.Dispose();
        FriendListController.DestroyFriend(_guideKey);
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
    ///                     int sceneIndex;
    /// </summary>
    public static void HandleMatchFound(byte[] data)
    {   
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            Network.InBattle(buffer.ReadInteger());
        }
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
        buffer.ReadString(); // this's race, use it bro 
        int spellcount = buffer.ReadInteger();
        short[] _skillArray = new short[spellcount/2];
        short[] _skillVariation = new short[spellcount/2];
        for (int i = 0; i < spellcount/2; i++)
        {
            _skillArray[i] = buffer.ReadShort();
            _skillVariation[i] = buffer.ReadShort();
        }
        buffer.Dispose();
        ArmoryController.SetSkills(_skillArray, _skillVariation);
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
        int playersCount = buffer.ReadInteger();
        string[] nicknames = new string[playersCount];
        float[][] positions = new float[playersCount][];
        float[][] rotations = new float[playersCount][];
        for(int i = 0; i < playersCount; i++)
        {
            nicknames[i] = buffer.ReadString();
            positions[i] = new float[] { buffer.ReadFloat(), 0.5f, buffer.ReadFloat() };
            rotations[i] = new float[] { buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat() };
        }
        buffer.Dispose();
        MainThread.executeInUpdate(() => { BattleLoader.SpanwPlayers(nicknames, positions, rotations, playersCount); });
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
        int _amountOfBuilds = buffer.ReadInteger();
        short[][][] spells = new short[_amountOfBuilds][][];
        for(int i = 0; i < _amountOfBuilds; i++)
        {
            int length = buffer.ReadInteger();
            short[] containerHash = new short[length];
            short[] variationHash = new short[length];
            for (int j = 0; j < length; j++)
            {
                containerHash[j] = buffer.ReadShort();
                variationHash[j] = buffer.ReadShort();
            }
            spells[i] = new short[][] { containerHash, variationHash };
        }
        buffer.Dispose();
        MainThread.executeInUpdate(() => BattleLoader.LoadSpells(spells));
    }

    public static void HandleInstantiate(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        int _prefabIndex = buffer.ReadInteger();
        int _objectIndex = buffer.ReadInteger();
        int _instanceIndex = buffer.ReadInteger();
        float[] _castPos = new float[] { buffer.ReadFloat(), buffer.ReadFloat(), buffer.ReadFloat() };
        float[] _targetPos = buffer.ReadVector3();
        float[] _rot = buffer.ReadQuternion();
        int hp = buffer.ReadInteger();
        string _casterNickname = buffer.ReadString();
        MainThread.executeInUpdate(() => Network.NetworkInstantiate(_prefabIndex, _objectIndex, _instanceIndex, _castPos, _targetPos, _rot, hp, _casterNickname));
        buffer.Dispose();
    }

    public static void HandleDestroy(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInteger();
        ObjectType type = (ObjectType)buffer.ReadInteger();
        int index = buffer.ReadInteger();
        switch(type)
        {
            case ObjectType.spell:
                MainThread.executeInUpdate(() => Network.currentManager.dynamicPropList.Remove(index));
                break;
        }
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


    private static void HandleDamage(byte[] data)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            ObjectType type = (ObjectType)buffer.ReadInteger();
            int _index = buffer.ReadInteger();
            int _physicDamage = buffer.ReadInteger();
            int _ignisDamage = buffer.ReadInteger();
            int _terraDamage = buffer.ReadInteger();
            int _caeliDamage = buffer.ReadInteger();
            int _aquaDamage = buffer.ReadInteger();
            int _pureDamage = buffer.ReadInteger();
            bool _damageType = buffer.ReadBoolean();
            switch (type)
            {
                case ObjectType.player:
                    Network.currentManager.Players[_index].GetDamage(_physicDamage,_ignisDamage, _terraDamage, _caeliDamage, _aquaDamage, _pureDamage, _damageType);
                    break;
                case ObjectType.spell:
                    Network.currentManager.dynamicPropList[_index].GetDamage(_physicDamage, _ignisDamage, _terraDamage, _caeliDamage, _aquaDamage, _pureDamage, _damageType);
                    break;
            }
        }
    }

    #endregion

}

