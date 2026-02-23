namespace TrProtocol.Packets;

public class UpdatePlayerLuckFactors : Packet, IPlayerSlot
{
    public override MessageID Type => MessageID.UpdatePlayerLuckFactors;
    public byte PlayerSlot { get; set; }
    public int LadyBugTime { get; set; }
    public float Torch { get; set; }
    public byte Potion { get; set; }
    public bool HasGardenGnomeNearby { get; set; }
    public bool BrokenMirrorBadLuck { get; set; }
    public float Equip { get; set; }
    public float Coin { get; set; }
    public byte KiteLuckLevel { get; set; }
}