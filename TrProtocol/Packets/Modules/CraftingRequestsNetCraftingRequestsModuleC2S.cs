namespace TrProtocol.Packets.Modules;

[C2SOnly]
public class CraftingRequestsNetCraftingRequestsModuleC2S : NetModulesPacket
{
    public override MessageID Type => MessageID.NetModules;
    public override NetModuleType ModuleType => NetModuleType.CraftingRequestsNetCraftingRequestsModule;
    public RequiredItemData RequiredItemData { get; set; }

}
