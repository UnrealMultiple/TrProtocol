using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class RequestSection : Packet
{
    public override MessageID Type => MessageID.RequestSection;
    public UShortPosition Position { get; set; }
}
