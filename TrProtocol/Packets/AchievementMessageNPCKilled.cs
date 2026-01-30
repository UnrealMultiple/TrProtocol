namespace TrProtocol.Packets;

public class AchievementMessageNPCKilled : Packet
{
    public override MessageID Type => MessageID.NotifyPlayerNpcKilled;
    public short NPCType { get; set; }
}