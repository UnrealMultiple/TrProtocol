using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

internal class NPCDebuffDamage : Packet
{
    public override MessageID Type => MessageID.NPCDebuffDamage;
    public byte NPCSlot { get; set; }
    public short Amount { get; set; }
}
