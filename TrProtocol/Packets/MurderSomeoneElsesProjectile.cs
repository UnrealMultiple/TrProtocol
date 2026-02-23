namespace TrProtocol.Packets;

public class MurderSomeoneElsesProjectile : Packet
{
    public override MessageID Type => MessageID.MurderSomeoneElsesProjectile;
    public ushort OtherPlayerSlot { get; set; }
    public byte AI1 { get; set; }
}