using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUDPSendData : MonoBehaviour {

    public static void SendConnectionOk()
    {
        PacketBuffer buffer = new PacketBuffer(); 
        buffer.WriteInteger((int)UDPRoomPackets.URConnectionComplite);
        //buffer.WriteInteger(RoomTCP.GetPlayerIndex()); // unusable
        //buffer.WriteInteger(RoomTCP.Getindex());
        RoomUDP.SendData(buffer.ToArray());
        DeveloperScreenController.AddInfo("Udp Conection Ok Sended", 1);
        buffer.Dispose();
    }
}
