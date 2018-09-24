using UnityEngine;

public class SendDataUDP : MonoBehaviour {

    public static void SendConnectionOk()
    {
        PacketBuffer buffer = new PacketBuffer(); 
        buffer.WriteInteger((int)UDPRoomPackets.URConnectionComplite);
        ClientUDP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendPositionUpdate(ObjectType objType, int objIndex, int index, Vector3 position)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URPositionUpdate);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            buffer.WriteVector3(new float[] { position.x, position.y, position.z });
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendPositionStepback(ObjectType objType, int objIndex, int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URPositionStepback);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendRotationUpdate(ObjectType objType, int objIndex, int index, Quaternion rotation)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URRotationUpdate);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            buffer.WriteQuaternion(new float[] { rotation.x, rotation.y, rotation.z, rotation.w });
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendRotationStepback(ObjectType objType, int objIndex, int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URRotationStepback);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendHPUpdate(ObjectType objType, int objIndex, int index, int hp)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URHealthUpdate);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            buffer.WriteInteger(hp);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendHPStepback(ObjectType objType, int objIndex, int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URHealthStepback);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendSNUpdate(ObjectType objType, int objIndex, int index, int hp)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URSynergyUpdate);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            buffer.WriteInteger(hp);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

    public static void SendSNStepback(ObjectType objType, int objIndex, int index)
    {
        using (PacketBuffer buffer = new PacketBuffer())
        {
            buffer.WriteInteger((int)UDPRoomPackets.URSynergyStepback);
            buffer.WriteInteger((int)objType);
            buffer.WriteInteger(objIndex);
            buffer.WriteInteger(index);
            ClientUDP.SendData(buffer.ToArray());
        }
    }

}
