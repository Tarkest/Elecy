using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class RoomTCP : MonoBehaviour {

    private static int index;
    private static bool receiving = false;
    private static byte[] _buffer = new byte[NetworkConstants.BUFFER_SIZE];
    private static Socket socket;

    public static void InitRoom(int roomindex)
    {
        socket = NetPlayerTCP.GetSocket();
        index = roomindex;
    }

    public static void BeginReceive()
    {
        //RoomSendData.SendConnectionOk(index);
        Debug.Log("Room begin receive");
        receiving = true;
        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RoomReceiveCallback), socket);
    }

    public static void RoomReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Debug.Log("Room receiving");
            if (socket.Connected)
            {
                int received = socket.EndReceive(ar);
                if (received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(_buffer, data, received);
                    RoomHandleNetworkInformation.HandleNetworkInformation(data);
                    if (receiving)
                        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RoomReceiveCallback), socket);
                    else
                        return;
                }
                else
                {
                    socket.Close();
                    EntranceController.serverInfo = "Room received nothing. Connection aborded...";
                }
            }

        }
        catch
        {
            socket.Close();
            EntranceController.serverInfo = "Room receive exception";
        }
    }

    public static void Stop()
    {
        receiving = false;
        // Send to server close
    }

    public static bool isConnected()
    {
        return receiving;
    }

    public static void Close()
    {
        if (receiving)
            Stop();
        socket = null;
    }

    public static void SendData(byte[] data)
    {
        socket.Send(data);
    }

    public static int Getindex()
    {
        return index;
    }
}
