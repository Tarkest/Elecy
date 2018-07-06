using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class RoomTCP : MonoBehaviour {

    private static int index;
    private static bool receiving = false;
    private static byte[] _buffer = new byte[NetworkConstants.TCP_BUFFER_SIZE];
    private static Socket socket;
    private static int _inRoomIndex;

    public static void InitRoom(int roomindex)
    {
        socket = NetPlayerTCP.GetSocket();
        index = roomindex;
    }

    public static void BeginReceive()
    {
        receiving = true;
        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RoomReceiveCallback), socket);
    }

    public static void RoomReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (socket.Connected)
            {
                int received = socket.EndReceive(ar);
                if (received > 0)
                {
                    byte[] data = new byte[received];
                    Array.Copy(_buffer, data, received);
                    if (receiving)
                        socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RoomReceiveCallback), socket);
                    else
                        return;
                    RoomHandleNetworkInformation.HandleNetworkInformation(data);
                }
                else
                {
                    socket.Close();
                    DeveloperScreenController.AddInfo("Room received nothing. Connection aborded...", 1);
                }
            }

        }
        catch
        {
            socket.Close();
            EntranceController.GetError("Room receive exception");
        }
    }

    public static void Stop()
    {
        receiving = false;
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

    public static float[] GetBattlegroundScale()
    {
        float[] scale = new float[2];
        scale[0] = 5f;
        scale[1] = 5f;

        return scale;
    }

    public static void SetPlayerIndex(int index)
    {
        _inRoomIndex = index;
    }

    public static int GetPlayerIndex()
    {
        return _inRoomIndex;
    }
}
