using System;
using System.Net.Sockets;


public static class ClientTCP
{

    #region Variables

    public static GameState clientState;
    public static PlayerState playerState;
    public static int[] levels;
    public static int[] ranks;
    public static string nickname;

    private static Socket _socket;
    private static byte[] _buffer;

    #endregion

    #region Enum

    public enum GameState
    {
        Sleep,
        Entrance,
        MainLobby,
        GameArena,
    }

    public enum PlayerState
    {
        MainLobby,
        Loading,
        Playing,
        EndPlaying,
    }

    #endregion

    #region Initialization

    public static void Init()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _buffer = new byte[NetworkConstants.TCP_BUFFER_SIZE];
        EntranceController.GetInProcess("Connecting...");
        clientState = GameState.Entrance;
        Network.Connect = Network.ConnectStatus.Connecting;
    }

    public static void Login(string nick, int[][] data)
    {
        nickname = nick;
        levels = data[0];
        ranks = data[1];
        clientState = GameState.MainLobby;
        playerState = PlayerState.MainLobby;
        SendDataTCP.SendLoginComplite();
    }

    public static void EnterRoom()
    {
        clientState = GameState.MainLobby;
        playerState = PlayerState.Loading;
        SendDataTCP.SendEnterRoom();
    }

    public static void LeaveRoom()
    {
        clientState = GameState.MainLobby;
        playerState = PlayerState.MainLobby;
    }

    #endregion

    #region Connect

    public static void Connect(string IP_ADDRESS, int PORT)
    {
        _socket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallback), null);
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        _socket.EndConnect(ar);
        ConnectReceive();
    }

    private static void ConnectReceive()
    {
        byte[] _sizeInfo = new byte[4];
        byte[] receivedBuffer;

        int totalRead = 0, currentRead = 0;

        try
        {
            currentRead = totalRead = _socket.Receive(_sizeInfo);
            if (totalRead <= 0)
            {
                Network.Connect = Network.ConnectStatus.Unconnected;
                EntranceController.GetError("Server unavalible.");
                Close();
            }
            else
            {
                while (totalRead < _sizeInfo.Length && currentRead > 0)
                {
                    currentRead = _socket.Receive(_sizeInfo, totalRead, _sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                int messageSize = 0;
                messageSize |= _sizeInfo[0];
                messageSize |= (_sizeInfo[1] << 8);
                messageSize |= (_sizeInfo[2] << 16);
                messageSize |= (_sizeInfo[3] << 24);
                receivedBuffer = new byte[messageSize];
                totalRead = 0;
                currentRead = totalRead = _socket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                while (totalRead < messageSize && currentRead > 0)
                {
                    currentRead = _socket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                HandleDataTCP.HandleNetworkInformation(receivedBuffer);
                Receive();
                Network.Connect = Network.ConnectStatus.Connected;
            }
        }
        catch
        {
            Network.Connect = Network.ConnectStatus.Unconnected;
            EntranceController.GetError("Server unavalible.");
            Close();
        }
    }

    #endregion

    #region Receive

    public static void Receive(GameState? s = null)
    {
        clientState = s ?? clientState;
        if(clientState != GameState.Sleep)
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _socket);
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientState != GameState.Sleep)
            {
                int received = _socket.EndReceive(ar);
                if (received > 0)
                {
                    if (clientState != GameState.Sleep)
                        _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _socket);
                    byte[] data = new byte[received];
                    Array.Copy(_buffer, data, received);
                    HandleDataTCP.HandleNetworkInformation(data);
                }
                else
                {
                    Network.Connect = Network.ConnectStatus.Unconnected;
                    EntranceController.GetError("Server unavalible.");
                    Close();
                }
            }
        }
        catch (Exception ex)
        {
            Network.Connect = Network.ConnectStatus.Unconnected;
            EntranceController.GetError(ex + "");
            Close();
        }
    }

    #endregion

    #region SendData

    public static void SendData(byte[] data)
    {
        try
        {
            _socket.Send(data);
        }
        catch
        {
            EntranceController.GetInProcess("Can`t connect to the server /n Reconnecting...");
            Network.Connect = Network.ConnectStatus.Connecting;
        }
    }

    public static void SendLoadingData(byte[] data)
    {
        try
        {
            System.Threading.Thread.Sleep(250);
            _socket.Send(data);
        }
        catch
        {
            EntranceController.GetInProcess("Can`t connect to the server /n Reconnecting...");
            Network.Connect = Network.ConnectStatus.Connecting;
        }
    }

    #endregion

    #region Finalization

    public static void Close()
    {
        clientState = GameState.Sleep;
        try
        {
            _socket.Close();
        }
        catch { }
    }

    #endregion

}

