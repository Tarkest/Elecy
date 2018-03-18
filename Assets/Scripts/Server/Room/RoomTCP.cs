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
    private static List<NetworkGameObject> GameObjects;

    public static void InitRoom(int roomindex)
    {
        socket = NetPlayerTCP.GetSocket();
        index = roomindex;
        GameObjects = new List<NetworkGameObject>();
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

    public static void LoadRocks(int rocksCount, int[] indexes, Vector3[] rocksPosition, Quaternion[] rocksRotation)
    {
        for(int i = 0; i == rocksCount; i++)
        {
            GameObject NewRock = Resources.Load("/BattleArena/Rock") as GameObject;
            NetworkGameObject NewRockNet = NewRock.AddComponent<NetworkGameObject>();
            NewRockNet.SetIndex(indexes[i]);
            NewRockNet.SetTransform(rocksPosition[i], rocksRotation[i]);
            GameObjects.Add(NewRockNet);
            Instantiate(NewRock, rocksPosition[i], rocksRotation[i]);
        }
        RoomSendData.SendRocksSpawned();
        BattleLoader.ThisPlayerProgressChange(0.66f);
    }

    public static void LoadTrees(int treesCount, int[] indexes, Vector3[] treesPosition, Quaternion[] treesRotation)
    {
        for(int i = 0; i == treesCount; i++)
        {
            GameObject NewTree = Resources.Load("/BattleArena/Tree") as GameObject;
            NetworkGameObject NewTreeNet = NewTree.AddComponent<NetworkGameObject>();
            NewTreeNet.SetIndex(indexes[i]);
            NewTreeNet.SetTransform(treesPosition[i], treesRotation[i]);
            GameObjects.Add(NewTreeNet);
            Instantiate(NewTree, treesPosition[i], treesRotation[i]);
        }
        RoomSendData.SendTreesSpawned();
        BattleLoader.ThisPlayerProgressChange(1f);
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
        scale[0] = GameObject.Find("Terrain").GetComponent<Transform>().lossyScale.x;
        scale[1] = GameObject.Find("Terrain").GetComponent<Transform>().lossyScale.z;

        return scale;
    }
}
