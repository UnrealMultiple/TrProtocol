using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets.Modules;

[S2COnly]
public class CraftingRequestsNetCraftingRequestsModuleS2C : NetModulesPacket
{
    public override MessageID Type => MessageID.NetModules;
    public override NetModuleType ModuleType => NetModuleType.CraftingRequestsNetCraftingRequestsModule;
    public bool Approved { get; set; }
}
