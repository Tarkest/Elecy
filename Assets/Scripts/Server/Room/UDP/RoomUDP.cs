﻿using System;
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
    
    public static void Create()
    {
        RoomUDPHandleNetworkInformation.InitializeNetworkPackages();
        _udpClient = new UdpClient();
        _ipAdress = new IPEndPoint(IPAddress.Parse(Network.IP_ADDRESS), Network.UDP_PORT);
        _udpClient.Connect(_ipAdress);

    }

    public static void BeginReceive()
    {
        _receiving = true;
        _udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), _udpClient);
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
            _udpClient.Send(data, data.Length);
        }
        catch (Exception ex)
        {
            _receiving = false;
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
            _receiving = false;
            DeveloperScreenController.AddInfo("UDP Recieve exception", 1);
            Debug.Log(ex + "");
        }
        if(_receiving)
            _udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), _udpClient);
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
