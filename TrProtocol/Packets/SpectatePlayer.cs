using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Packets;

public class SpectatePlayer : Packet, IPlayerSlot
{
    public override MessageID Type => MessageID.SpectatePlayer;

    public byte PlayerSlot { get; set; }
    public short TargetPlayer { get; set; }
}
