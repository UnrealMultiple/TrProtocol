namespace TrProtocol.Packets;

public class UnusedStrikeNPC : Packet, INPCSlot, IPlayerSlot
{
    public override MessageID Type => MessageID.UnusedStrikeNPC;
    public short NPCSlot { get; set; }
    public byte PlayerSlot { get; set; }
}
