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
    private static int _thisPlayerIndex;

    public static void InitRoom(int roomindex, int playerIndex)
    {
        socket = NetPlayerTCP.GetSocket();
        index = roomindex;
        _thisPlayerIndex = playerIndex;
    }

    public static void BeginReceive()
    {
        //RoomSendData.SendConnectionOk(index);
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
                    //EntranceController.serverInfo = "Room received nothing. Connection aborded...";
                }
            }

        }
        catch
        {
            socket.Close();
            EntranceController.GetError("Room receive exception");
        }
    }


    public static void InstantiateNetworkObject()
    {
        ///Here Comes the instantiation the object on the scene
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

    public static float[] GetBattlegroundScale()
    {
        float[] scale = new float[2];
        scale[0] = 5f;
        scale[1] = 5f;

        return scale;
    }
}
