using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientTCP : MonoBehaviour {

    public bool connectToLocal;

    [System.NonSerialized]
    public string IP_ADDRESS;
    [System.NonSerialized]
    public int PORT = 24985;

    private int ipnum;

    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private byte[] _asyncBuffer = new byte[1024];

    private void Start()
    {
        if(connectToLocal == true)
        {
            IP_ADDRESS = "127.0.0.1";
        }
        else
        {
            IP_ADDRESS = "77.122.14.86";
        }

        clientSocket.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallBack), clientSocket);
        EntranceController.serverInfo = "Connecting to the server...";
    }

    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    private void ConnectCallBack(IAsyncResult ar)
    {
        clientSocket.EndConnect(ar);
        OnRecive();
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
                EntranceController.serverInfo = "Server unavalible.";
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
                clientSocket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(RecievedCallBack), null);
            }
        }
        catch
        {
            EntranceController.serverInfo = "Server unavalible.";
        }
    }

    private void RecievedCallBack(IAsyncResult ar)
    {
        try
        {
            Debug.Log("I'm recieving");
            int received = clientSocket.EndReceive(ar);

            if (received <= 0)
            {
                EntranceController.serverInfo = "Server unavalible.";
            }
            else
            {
                byte[] dataBuffer = new byte[received];
                Debug.Log(received);
                Array.Copy(_asyncBuffer, dataBuffer, received);
                // Debug
                PacketBuffer buffer = new PacketBuffer();
                buffer.WriteBytes(_asyncBuffer);
                Debug.Log("Recieved int: " + buffer.ReadInteger() + " | username: " + buffer.ReadString());
                ClientHandlerNetworkData.HandleNetworkInformation(dataBuffer);
                clientSocket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(RecievedCallBack), null);
            }
        }
        catch
        {
            Console.WriteLine("catch");
            //Connection aborted
        }
    }

    public static void SendData(byte[] data)
    {
        clientSocket.Send(data);
    }
}
