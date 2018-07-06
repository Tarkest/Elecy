using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;


public class RoomUDP : MonoBehaviour
{
    private static UdpClient _udpClient;
    private static bool _receiving = false;

    private static IPEndPoint _ipAdress;
    private static int _port;
    

    public static void Create(string IPAdress, int Port)
    {
        _udpClient = new UdpClient();
        _ipAdress = new IPEndPoint(IPAddress.Parse(IPAdress), Port);
    }

    public static void BeginReceive()
    {
        _receiving = true;
        _udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        RoomUDPSendData.SendConnectionOk();
    }

    public static void Stop()
    {
        _receiving = false;
    }

    public static void Close()
    {
        _udpClient.Close();
    }

    public static void SendData(byte[] data)
    {
        try
        {
            _udpClient.Send(data, data.Length, _ipAdress);
        }
        catch (Exception ex)
        {
            DeveloperScreenController.AddInfo("UDP Recieve exception", 1);
            Debug.Log(ex + "");
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if(_receiving)
            {
                IPEndPoint _ipEndPointOfThis = null;
                byte[] data = _udpClient.EndReceive(ar, ref _ipEndPointOfThis);
                RoomUDPHandleNetworkInformation.HandleNetworkInformation(data);
            }
        }
        catch (Exception ex)
        {
            DeveloperScreenController.AddInfo("UDP Recieve exception", 1);
            Debug.Log(ex + "");
        }
        BeginReceive();
    }

    public static bool IsConnected()
    {
        return _receiving;
    }

    public static UdpClient UDPClient()
    {
        return _udpClient;
    }
}
