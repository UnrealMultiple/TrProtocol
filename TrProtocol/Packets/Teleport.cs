namespace TrProtocol.Packets;

public class Teleport : Packet
{
    public override MessageID Type => MessageID.Teleport;
    public BitsByte Bit1 { get; set; }
    public short PlayerSlot { get; set; }
    public Vector2 Position { get; set; }
    public byte Style { get; set; }
    [Condition(nameof(Bit1), 3)] public int ExtraInfo { get; set; }
}