using UnityEngine;

public class SendDataUDP : MonoBehaviour {

    public static void SendConnectionOk()
    {
        PacketBuffer buffer = new PacketBuffer(); 
        buffer.WriteInteger((int)UDPRoomPackets.URConnectionComplite);
        ClientUDP.SendData(buffer.ToArray());
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
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendMoveBack(int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URTransformStepback);
            buffer.WriteInteger(index);
            ClientUDP.SendData(buffer.ToArray());
        }
    }
}
