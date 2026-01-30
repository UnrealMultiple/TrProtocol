using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class ExtraSpawnSectionLoaded : Packet, IPlayerSlot
{
    public override MessageID Type => MessageID.ExtraSpawnSectionLoaded;
    public byte PlayerSlot { get; set; }
}
