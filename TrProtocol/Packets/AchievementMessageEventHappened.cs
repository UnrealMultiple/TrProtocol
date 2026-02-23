namespace TrProtocol.Packets;

public class AchievementMessageEventHappened : Packet
{
    public override MessageID Type => MessageID.NotifyPlayerOfEvent;
    public short EventType { get; set; }
}