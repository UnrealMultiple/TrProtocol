namespace TrProtocol.Packets;

public class SpawnBoss : Packet
{
    public override MessageID Type => MessageID.SpawnBossorInvasion;
    public short OtherPlayerSlot { get; set; }
    public short NPCType { get; set; }
}