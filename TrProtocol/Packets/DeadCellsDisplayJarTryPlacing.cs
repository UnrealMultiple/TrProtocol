using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class DeadCellsDisplayJarTryPlacing : Packet
{
    public override MessageID Type => MessageID.DeadCellsDisplayJarTryPlacing;
    public ShortPosition Position { get; set; }
    public short ItemType { get; set; }
    public byte Prefix { get; set; }
    public int Stack { get; set; }

}
