namespace TrProtocol.Packets;

public class AddPlayerBuff : Packet, IOtherPlayerSlot
{
    public override MessageID Type => MessageID.PlayerAddBuff;
    public byte OtherPlayerSlot { get; set; }
    public ushort BuffType { get; set; }
    public int BuffTime { get; set; }
}