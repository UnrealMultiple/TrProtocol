namespace TrProtocol.Models;

public partial struct RequiredItemData
{
    public int ItemCount { get; set; }

    public RequiredItemEntry[] ItemEntries { get; set; }

    public int ChestCount { get; set; }

    public int[] ChestSlots { get; set; }
}
