using UnityEngine;
using System;

public class RoomUDPSendData : MonoBehaviour {

    public static void SendConnectionOk()
    {
        PacketBuffer buffer = new PacketBuffer(); 
        buffer.WriteInteger((int)UDPRoomPackets.URConnectionComplite);
        RoomUDP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendMovePosition(ObjectType objType, int objIndex, int index, Vector3 position)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URTransformUpdate);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            buffer.WriteVector3(new float[] { position.x, position.y, position.z });
            RoomUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendMoveBack(int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URTransformStepback);
            buffer.WriteInteger(index);
            RoomUDP.SendData(buffer.ToArray());
        }
    }
}
