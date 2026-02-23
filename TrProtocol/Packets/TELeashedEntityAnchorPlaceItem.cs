using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class TELeashedEntityAnchorPlaceItem : Packet
{
    public override MessageID Type => MessageID.TELeashedEntityAnchorPlaceItem;
    public Position Position { get; set; }
    public short ItemType { get; set; }

}
