internal class SendDataTCP
{

    #region Entrance

    /// <summary>
    ///             Buffer:
    ///                     int PakcetNum;
    /// </summary>
    public static void SendConnectionComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CConnectComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string username;
    ///                     string password;
    /// </summary>
    public static void SendLogin(string name, string password)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CLoginTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string username;
    ///                     string password;
    ///                     string nickname;
    /// </summary>
    public static void SendRegister(string name, string password, string nickname)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CRegisterTry);
        buffer.WriteString(name);
        buffer.WriteString(password);
        buffer.WriteString(nickname);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    #endregion

    #region MainLobby

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendLoginComplite()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PConnectionComplite);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string message;
    /// </summary>
    public static void SendGlChatMsg(string message)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGlChatMsg);
        buffer.WriteString(message);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int matchType;
    ///                     string race;
    /// </summary>
    public static void SendQueueStart(int MatchType, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStart);
        buffer.WriteInteger(MatchType);
        buffer.WriteString(race);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendQueueStop()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PQueueStop);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string race;
    /// </summary>
    public static void SendGetSkillBuild(string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PGetSkillsBuild);
        buffer.WriteString(race);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     string race;
    ///                     int spellCount;
    ///                     int[spellCount] spellIndex;
    /// </summary>
    public static void SendSaveSkillBuild(short[] Build, short[] Variation, string race)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)NetPlayerPackets.PSaveSkillsBuild);
        buffer.WriteString(race);
        buffer.WriteInteger(Build.Length);
        for (int i = 0; i < Build.Length; i++)
        {
            buffer.WriteShort(Build[i]);
            buffer.WriteShort(Variation[i]);
        }
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendTestRoomEnter(int mapIndex, string race)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)NetPlayerPackets.PTestRoom);
            buffer.WriteInteger(mapIndex);
            buffer.WriteString(race);
            ClientTCP.SendData(buffer.ToArray());
        }
    }

    public static void SendFriendTag(string guideTag)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)NetPlayerPackets.PAddFriend);
            buffer.WriteString(guideTag);
            ClientTCP.SendData(buffer.ToArray());
        }
    }

    #endregion

    #region GameRoom

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendEnterRoom()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RConnectionComplite);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendBeginLoading(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetPlayers);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    ///                     int RocksTypeCount;
    ///                     bool BigExist;
    ///                     bool MiddleExist;
    ///                     bool SmallExist;
    /// </summary>
    public static void SendSpawnRocks(float LoadProgress, int RocksTypeCount, bool BigExist, bool MiddleExist, bool SmallExist)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetRocks);
        buffer.WriteFloat(LoadProgress);
        buffer.WriteInteger(RocksTypeCount);
        buffer.WriteBoolean(BigExist);
        buffer.WriteBoolean(MiddleExist);
        buffer.WriteBoolean(SmallExist);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    ///                     int TreesTypeCount;
    ///                     bool BigExist;
    ///                     bool MiddleExist;
    ///                     bool SmallExist;
    /// </summary>
    public static void SendSpawnTrees(float LoadProgress, int TreesTypeCount, bool BigExist, bool MiddleExist, bool SmallExist)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetTrees);
        buffer.WriteFloat(LoadProgress);
        buffer.WriteInteger(TreesTypeCount);
        buffer.WriteBoolean(BigExist);
        buffer.WriteBoolean(MiddleExist);
        buffer.WriteBoolean(SmallExist);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendGetSpells(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RGetSpells);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     float loadProgress;
    /// </summary>
    public static void SendLoadComplite(float LoadProgress)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RLoadComplite);
        buffer.WriteFloat(LoadProgress);
        ClientTCP.SendLoadingData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendSurrender()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RSurrender);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    /// </summary>
    public static void SendRoomLeave()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RRoomLeave);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int index;
    ///                     int instanceIndex;
    ///                     Vector3 position;
    ///                     Quaternion rotation;
    ///                     int hp;
    /// </summary>
    public static void SendInstantiate(int index, int instanceIndex, float[] castPosition, float[] targetPosition, float[] rotation, int hp)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RInstantiate);
        buffer.WriteInteger(index);
        buffer.WriteInteger(instanceIndex);
        buffer.WriteVector3(castPosition);
        buffer.WriteVector3(targetPosition);
        buffer.WriteQuaternion(rotation);
        buffer.WriteInteger(hp);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    /// <summary>
    ///             Buffer:
    ///                     int PacketNum;
    ///                     int ObjectType;
    ///                     int index;
    /// </summary>
    public static void SendDestroy(ObjectType type, int index)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)RoomPackets.RDestroy);
        buffer.WriteInteger((int)type);
        buffer.WriteInteger(index);
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendDamage(ObjectType type, int index, int PhysicDamage, int IgnisDamage, int TerraDamage, int AquaDamage, int CaeliDamage, int PureDamage, bool heal, StaticTypes? staticType = null)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)RoomPackets.RDamage);
            buffer.WriteInteger((int)type);
            if (staticType != null)
                buffer.WriteInteger((int)staticType);
            buffer.WriteInteger(index);
            buffer.WriteInteger(PhysicDamage);
            buffer.WriteInteger(IgnisDamage);
            buffer.WriteInteger(TerraDamage);
            buffer.WriteInteger(AquaDamage);
            buffer.WriteInteger(CaeliDamage);
            buffer.WriteInteger(PureDamage);
            buffer.WriteBoolean(heal);
            ClientTCP.SendData(buffer.ToArray());
        }
    }

    #endregion

}

