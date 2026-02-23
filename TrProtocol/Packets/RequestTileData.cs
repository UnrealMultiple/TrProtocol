namespace TrProtocol.Packets;

public class RequestTileData : Packet
{
    public override MessageID Type => MessageID.RequestTileData;
    public Position Position { get; set; }
    public byte Team { get; set; }
}
