using System;
using System.Net.Sockets;
using System.Threading;

public static class NetPlayerTCP
{
    public static int[] level = new int[NetworkConstants.RACES_NUMBER];
    public static int[] rank = new int[NetworkConstants.RACES_NUMBER];

    private static byte[] _buffer = new byte[NetworkConstants.BUFFER_SIZE];
    private static int index;
    private static string ip;
    private static string nickname;
    //private static playerState state; State for checking where player is
    private static bool receiving = false;
    private static Socket socket;

    public enum playerState
    {
        InMainLobby = 1,
        SearchingForMatch = 2,
        Playing = 3,
        EndPlaying = 4
    }

    public static void InitPlayer(int playerIndex, string nick, int[][] data)
    {
        //state = playerState.InMainLobby;
        socket = ClientTCP.GetSocket();
        nickname = nick;
        index = playerIndex;
        level = data[0];
        rank = data[1];
    }

    public static void BeginReceive()
    {
        receiving = true;
        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlayerReceiveCallback), socket);
        NetPlayerSendData.SendConnectionComplpite();
    }

    public static void PlayerReceiveCallback(IAsyncResult ar)
    {
        try
        {
            socket = (Socket)ar.AsyncState;
            if (socket.Connected)
            {
                int received = socket.EndReceive(ar);
                if (received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(_buffer, data, received);
                    NetPlayerHandleNetworkData.HandleNetworkInformation(data);
                    if(receiving)
                        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlayerReceiveCallback), socket);
                    else
                        return;
                }
            }
        }
        catch
        {
            socket.Close();
            MainLobbyController.Error("Player receive exception");
        }
    }

    public static void Stop()
    {
        receiving = false;
        NetPlayerSendData.SendStopPlayer();
    }

    public static bool isConnected()
    {
        return receiving;
    }
 
    public static void Close()
    {
        if(receiving)
            Stop();
        socket = null;
    }

    public static void SendData(byte[] data)
    {
        try
        {
           socket.Send(data);
        }
        catch
        {
            MainLobbyController.Error("Can`t send packet to the server");
        }
    }

    #region Gets and Sets
    public static int GetIndex()
    {
        return index;
    }
    public static string GetNickname()
    {
        return nickname;
    }

    public static Socket GetSocket()
    {
        return socket;
    }
    #endregion
}