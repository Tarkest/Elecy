using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientTCP : MonoBehaviour {

    public string IP_ADDRESS;
    public int PORT;

    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private byte[] _asyncBuffer = new byte[1024];

    private void Start()
    {
        Debug.Log("Connecting to server...");
        clientSocket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallBack), clientSocket);
    }

    private void ConnectCallBack(IAsyncResult ar)
    {
        clientSocket.EndConnect(ar);
        while (true)
        {
            OnRecive();
        }
    }

    private void OnRecive()
    {
        byte[] _sizeInfo = new byte[4];
        byte[] _recivedBuffer = new byte[1024];

        int totalRead = 0, currentRead = 0;

        try
        {
            currentRead = totalRead = clientSocket.Receive(_sizeInfo);
            if (totalRead <= 0)
            {
                Debug.Log("You are not connected to the server!");
            }
            else
            {
                while(totalRead < _sizeInfo.Length && currentRead > 0)
                {
                    currentRead = clientSocket.Receive(_sizeInfo, totalRead, _sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }

                int messageSize = 0;
                messageSize |= _sizeInfo[0];
                messageSize |= (_sizeInfo[1] << 8);
                messageSize |= (_sizeInfo[2] << 16);
                messageSize |= (_sizeInfo[3] << 24);

                byte[] data = new byte[messageSize];

                totalRead = 0;
                currentRead = totalRead = clientSocket.Receive(data, totalRead, data.Length - totalRead, SocketFlags.None);
                while(totalRead < messageSize && currentRead > 0)
                {
                    currentRead = clientSocket.Receive(data, totalRead, data.Length - totalRead, SocketFlags.None);
                    totalRead += currentRead;
                }

                ClientHandlerNetworkData.HandleNetworkInformation(data);
            }
        }
        catch
        {
            Debug.Log("You are not connected to the server!");
        }
    }

    public static void SendData(byte[] data)
    {
        clientSocket.Send(data);
    }

    public static void ThankYouServer()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInteger((int)ClientPackets.CThankYou);
        buffer.WriteString("Thank vados for letting me connect to your 'server'.");
        SendData(buffer.ToArray());
        buffer.Dispose();
    }

}
