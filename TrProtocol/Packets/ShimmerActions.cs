namespace TrProtocol.Packets;

public class ShimmerActions : Packet
{
    public override MessageID Type => MessageID.ShimmerActions;
    public byte ShimmerType { get; set; }
    private bool IsItemShimmer => ShimmerType == 0;
    private bool IsPlayerShimmer => ShimmerType == 1;
    [Condition(nameof(IsItemShimmer))]
    public Vector2 ShimmerPosition { get; set; }
    [Condition(nameof(IsPlayerShimmer))]
    public Vector2 CoinPosition { get; set; }
    [Condition(nameof(IsPlayerShimmer))]
    public int CoinAmount { get; set; }
}