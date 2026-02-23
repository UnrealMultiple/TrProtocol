namespace TrProtocol.Packets;

public class TamperWithNPC : Packet, INPCSlot
{
    public override MessageID Type => MessageID.TamperWithNPC;
    public short NPCSlot { get; set; }
    public byte UniqueImmune { get; set; }
    private bool _isUniqueImmune => UniqueImmune == 1;

    [Condition(nameof(_isUniqueImmune))]
    public int Time { get; set; }
    [Condition(nameof(_isUniqueImmune))]
    public short FromWho { get; set; }
}