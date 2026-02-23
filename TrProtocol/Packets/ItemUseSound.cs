using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

internal class ItemUseSound : Packet, IPlayerSlot
{
    public override MessageID Type => MessageID.ItemUseSound;

    public byte PlayerSlot { get; set; }
}
