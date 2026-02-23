namespace TrProtocol.Packets;

public class HostToken : Packet
{
    public override MessageID Type => MessageID.HostToken;
    public string Token { get; set; }
}
