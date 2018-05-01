using System;
using System.Net.Sockets;
using System.Threading;

public static class ClientTCP
{
    private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static byte[] _asyncBuffer = new byte[NetworkConstants.BUFFER_SIZE];
    private static bool receiving = false;
    private static Timer _connectTimer;
    private static int _reconnectTry = 0;

    public static void Connect(string IP_ADDRESS, int PORT)
    {
        socket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallBack), socket);
    }

    public static void Close()
    {
        socket.Close();
    }

    public static void Stop(int  pIndex)
    {
        receiving = false;
        if(socket.Connected)
            ClientSendData.SendClose(pIndex);
    }

    static void ConnectTimerCallback(object o)
    {
        _reconnectTry += 1;
        Network.ChangeConnectionStatus(false);
        if (_reconnectTry == 5)
        {
            try
            {
                _connectTimer.Dispose();
            }
            catch { return; }
            EntranceController.GetError("Server unavaliable /n Try again later");
        }
    }

    public static bool IsConnected()
    {
        return receiving;
    }

    public static void BeginReceive()
    {
        receiving = true;
        socket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
    }

    public static void SendData(byte[] data)
    {
        try
        {
            socket.Send(data);
        }
        catch
        {
            EntranceController.GetInProcess("Can`t connect to the server /n Reconnecting...");
            _connectTimer = new Timer(ConnectTimerCallback, null, 3000, 3000);
            Network.ChangeConnectionStatus(false);
        }
    }

    public static Socket GetSocket()
    {
        return socket;
    }

    private static void ConnectCallBack(IAsyncResult ar)
    {
        socket.EndConnect(ar);
        receiving = true;
        _reconnectTry = 0;
        ConnectReceive();
        EntranceController.GetOffProcess();
        try
        {
            _connectTimer.Dispose();
        }
        catch { return; }
    }

    private static void ConnectReceive()
    {
        byte[] _sizeInfo = new byte[4];
        byte[] receivedBuffer;

        int totalRead = 0, currentRead = 0;

        try
        {
            currentRead = totalRead = socket.Receive(_sizeInfo);
            if (totalRead <= 0)
            {
                Network.ChangeConnectionStatus(false);
                EntranceController.GetError("Server unavalible.");
            }
            else
            {
                Network.ChangeConnectionStatus(true);
                while (totalRead < _sizeInfo.Length && currentRead > 0)
                {
                    currentRead = socket.Receive(_sizeInfo, totalRead, _sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                int messageSize = 0;
                messageSize |= _sizeInfo[0];
                messageSize |= (_sizeInfo[1] << 8);
                messageSize |= (_sizeInfo[2] << 16);
                messageSize |= (_sizeInfo[3] << 24);
                receivedBuffer = new byte[messageSize];
                totalRead = 0;
                currentRead = totalRead = socket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                while (totalRead < messageSize && currentRead > 0)
                {
                    currentRead = socket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                ClientHandlerNetworkData.HandleNetworkInformation(receivedBuffer);
                BeginReceive();
            }
        }
        catch
        {
            Network.ChangeConnectionStatus(false);
            EntranceController.GetError("Server unavalible.");
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (socket.Connected)
            {
                int received = socket.EndReceive(ar);
                if (received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(_asyncBuffer, data, received);
                    ClientHandlerNetworkData.HandleNetworkInformation(data);
                    if (receiving)
                        socket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                    else
                        return;
                }
            }

        }
        catch
        {
            Network.ChangeConnectionStatus(false);
            EntranceController.GetError("Client receive exception");
            //socket.;
        }
    }


}
