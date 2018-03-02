using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientTCP : MonoBehaviour {

    public bool connectToLocal;

    [System.NonSerialized]
    public string IP_ADDRESS;
    [System.NonSerialized]
    public int PORT = 24985;

    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    public Socket socket;
    public static PlayerTCP player;

    private byte[] _asyncBuffer = new byte[1024];
    private static bool scenechange = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        if (connectToLocal == true)
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

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            Debug.Log("In if");
            player.StartPlayer();
        }
    }

    private void Update()
    {
        if (scenechange)
        {
            LoadScene();
        }
    }

    private void ConnectCallBack(IAsyncResult ar)
    {
        clientSocket.EndConnect(ar);
        OnRecive();
    }

    private void ReceiveCallback(IAsyncResult ar)
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
                    PacketBuffer packet = new PacketBuffer();
                    packet.WriteBytes(data);
                    int packetNum = packet.ReadInteger();
                    if (packetNum != 3)
                        socket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                    else
                        return;
                }
                else
                {
                    Debug.Log("ReceiveCallback fails!");
                    clientSocket.Close();
                }
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private void OnRecive()
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
                clientSocket.BeginReceive(_asyncBuffer, 0, _asyncBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
            }
        }
        catch
        {
            EntranceController.serverInfo = "Server unavalible.";
        }
    }

    private void LoadScene()
    {
        scenechange = false;
        SceneManager.LoadScene(2);
    }
  
    public static void SendData(byte[] data)
    {
        clientSocket.Send(data);
    }

    public static void ClientLogin()
    {
        ClientSendData.SendClose();
        player = new PlayerTCP();
        PlayerTCP.playerSocket = clientSocket;
        //clientSocket.Close();
        scenechange = true;
    }

    public static void ClientClose()
    {
        ClientSendData.SendClose();
    }

    void OnApplicationQuit()
    {
        if(clientSocket.Connected)
            ClientSendData.SendClose();
        clientSocket.Close();
        //DisconectFromServer
    }

    
}
