using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUDPSendData : MonoBehaviour {

    public static void SendConnectionOk()
    {
        PacketBuffer buffer = new PacketBuffer(); 
        buffer.WriteInteger((int)UDPRoomPackets.URConnectionComplite);
        RoomUDP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
