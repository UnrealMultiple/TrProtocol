using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class ItemPosition : Packet, IItemSlot
{
    public override MessageID Type => MessageID.ItemPosition;
    public short ItemSlot { get; set; }
    public Vector2 Position { get; set; }
}
