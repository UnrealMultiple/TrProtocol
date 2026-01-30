using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class SyncItemDespawn : Packet, IItemSlot
{
    public override MessageID Type => MessageID.SyncItemDespawn;

    public short ItemSlot { get; set; }
}
