using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class SyncChestSize : Packet
{
    public override MessageID Type => MessageID.SyncChestSize;
    public short ChestID { get; set; }
    public short Size { get; set; }
}
