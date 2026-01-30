namespace TrProtocol.Packets;

public class TeleportNPCThroughPortal : Packet
{
    public override MessageID Type => MessageID.TeleportNPCThroughPortal;
    public ushort NPCSlot { get; set; }
    public ushort Extra { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
}