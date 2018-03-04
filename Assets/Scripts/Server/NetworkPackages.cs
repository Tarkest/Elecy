public enum ServerPackets
{
    SConnectionOK = 1, SRegisterOK = 2, SLoginOK = 3, SAlert = 4, SGlChatMsg = 5
}

public enum ClientPackets
{
    CConnectComplite = 1, CRegisterTry = 2, CLoginTry = 3, CAlert = 4, CClose = 5, CReconnectComplite = 6
}

public enum PlayerPackets
{
    PConnectionComplite = 7, PGlChatMsg = 8
}

