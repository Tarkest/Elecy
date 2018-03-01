using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PlayerTCP
{
    public int index;
    public string ip;
    public string nickname;
    public bool playerClosing = false;
    public int[] level = new int[5];
    public int[] Rank = new int[5];
    public byte[] _buffer = new byte[1024];
    public playerState state;
    public static Socket playerSocket = null;

    public enum playerState
    {
        InMainLobby = 1,
        SearchingForMatch = 2,
        Playing = 3,
        EndPlaying = 4
    }

    public void StartPlayer()
    {
        Debug.Log("In start player");
        state = playerState.InMainLobby;
        playerSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlayerReceiveCallback), playerSocket);
        playerClosing = false;
    }

    //Check/rewrite (copy-paste from server)
    public void PlayerReceiveCallback(IAsyncResult ar)
    {
        playerSocket = (Socket)ar.AsyncState;
        Debug.Log("In player receive");

        try
        {
            int received = playerSocket.EndReceive(ar);

            if (received <= 0)
            {
                ClosePlayer();
            }
            else
            {
                byte[] dataBuffer = new byte[received];
                Array.Copy(_buffer, dataBuffer, received);
                //ServerHandleNetworkData.HandleNetworkInformation(index, dataBuffer);
                playerSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(PlayerReceiveCallback), playerSocket);
            }
        }
        catch
        {
            ClosePlayer();
        }
    }

    //Rewrite
    private void ClosePlayer()
    {
        playerClosing = true;
        //Console.WriteLine("Соединение от {0} было разорвано.", ip);
        //PlayerLeft();
        playerSocket.Close();
    }
}