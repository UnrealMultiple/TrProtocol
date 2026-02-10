namespace TrProtocol.Packets;

public class ChangeDoor : Packet
{
    public override MessageID Type => MessageID.DoorUse;
    public byte ChangeType { get; set; }
    public ShortPosition Position { get; set; }
    public byte Direction { get; set; }
}
