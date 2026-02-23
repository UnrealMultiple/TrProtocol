using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class TeamChangeFromUI : Packet, IPlayerSlot
{
    public override MessageID Type => MessageID.TeamChangeFromUI;
    public byte PlayerSlot { get; set; }
    public byte Team { get; set; }
}
