﻿public enum ServerPackets
{
    SConnectionOK = 1, SRegisterOK = 2, SLoginOK = 3, SAlert = 4, SGlChatMsg = 5, SQueueStarted = 6, SQueueContinue = 7, SMatchFound = 8, SRoomStarted = 9,
}

public enum ClientPackets
{
    CConnectComplite = 1, CRegisterTry = 2, CLoginTry = 3, CAlert = 4, CClose = 5, CReconnectComplite = 6
}

public enum NetPlayerPackets
{
    PConnectionComplite = 7, PGlChatMsg = 8, PQueueStart = 9, PSearch = 10, PBeginMatchLoad = 11, PQueueStop = 12, PAlert = 14,
}

public enum RoomPackets
{
    RConnectionComplite = 13
}