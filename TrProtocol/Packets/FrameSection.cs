namespace TrProtocol.Packets;

[Obsolete("Deprecated. Framing happens as needed after TileSection is sent.")]
public class FrameSection : Packet
{
    public override MessageID Type => MessageID.FrameSection;
    public ShortPosition Start { get; set; }
    public ShortPosition End { get; set; }
}
