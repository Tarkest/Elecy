using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


public class RoomUDP : MonoBehaviour
{
    private static Socket _socket;
    private static byte[] _asyncBuffer = new byte[NetworkConstants.UDP_BUFFER_SIZE];
    private static bool _receiving = false;

    public static void Create()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
    }

    public static void Connect(string IP_ADDRESS, int PORT)
    {
        _socket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallback), _socket);
    }

    public static void BeginReceive()
    {
        _receiving = true;
        _socket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _socket);
    }

    public static void Stop()
    {
        _receiving = false;
    }

    public static void Close()
    {
        _socket.Close();
    }

    public static void SendData(byte[] data)
    {
        try
        {
            _socket.Send(data);
        }
        catch
        {

        }
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
                DeveloperScreenController.AddInfo("UDP Connect exception", 1);
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
                RoomUDPHandleNetworkInformation.HandleNetworkInformation(receivedBuffer);
                BeginReceive();
            }
        }
        catch (Exception ex)
        {
            DeveloperScreenController.AddInfo("UDP Connect exception", 1);
            Debug.Log(ex + "");
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if(_receiving)
            {
                int received = _socket.EndReceive(ar);
                if(received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(_asyncBuffer, data, received);
                    RoomUDPHandleNetworkInformation.HandleNetworkInformation(data);
                    if(_receiving)
                        _socket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _socket);
                } 
            }
        }
        catch (Exception ex)
        {
            DeveloperScreenController.AddInfo("UDP Recieve exception", 1);
            Debug.Log(ex + "");
        }
    }

    public static bool IsConnected()
    {
        return _receiving;
    }

    public static Socket Socket()
    {
        return _socket;
    }
}
