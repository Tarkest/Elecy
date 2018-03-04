using System;
using System.Net.Sockets;

public static class ClientTCP
{
    private static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static byte[] _asyncBuffer = new byte[1024];
    private static bool receiving = false;

    public static void Connect(string IP_ADDRESS, int PORT)
    {
        clientSocket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallBack), clientSocket);
    }

    public static void Close()
    {
        Stop();
        clientSocket.Close();
    }

    public static void Stop()
    {
        receiving = false;
        ClientSendData.SendClose();
    }

    public static bool isConnected()
    {
        return clientSocket.Connected;
    }

    public static void BeginReceive()
    {
        receiving = true;
        clientSocket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
    }

    public static void SendData(byte[] data)
    {
        clientSocket.Send(data);
    }

    public static Socket GetSocket()
    {
        return clientSocket;
    }

    private static void ConnectCallBack(IAsyncResult ar)
    {
        clientSocket.EndConnect(ar);
        receiving = true;
        ConnectReceive();
    }

    private static void ConnectReceive()
    {
        byte[] _sizeInfo = new byte[4];
        byte[] receivedBuffer;

        int totalRead = 0, currentRead = 0;

        try
        {
            currentRead = totalRead = clientSocket.Receive(_sizeInfo);
            if (totalRead <= 0)
            {
                EntranceController.serverInfo = "Server unavalible.";
            }
            else
            {
                while (totalRead < _sizeInfo.Length && currentRead > 0)
                {
                    currentRead = clientSocket.Receive(_sizeInfo, totalRead, _sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                int messageSize = 0;
                messageSize |= _sizeInfo[0];
                messageSize |= (_sizeInfo[1] << 8);
                messageSize |= (_sizeInfo[2] << 16);
                messageSize |= (_sizeInfo[3] << 24);
                receivedBuffer = new byte[messageSize];
                totalRead = 0;
                currentRead = totalRead = clientSocket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                while (totalRead < messageSize && currentRead > 0)
                {
                    currentRead = clientSocket.Receive(receivedBuffer, totalRead, receivedBuffer.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }
                ClientHandlerNetworkData.HandleNetworkInformation(receivedBuffer);
                BeginReceive();
            }
        }
        catch
        {
            EntranceController.serverInfo = "Server unavalible.";
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        Socket socket;
        try
        {
            socket = (Socket)ar.AsyncState;
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
                else
                {
                    EntranceController.serverInfo = "Client received nothing. Connection aborded...";
                    clientSocket.Close();
                }
            }

        }
        catch
        {
            EntranceController.serverInfo = "Client receive exception";
            clientSocket.Close();
        }
    }

}
