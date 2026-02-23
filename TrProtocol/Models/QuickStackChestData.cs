using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Models;

public partial struct QuickStackChestData
{
    public int ItemCount { get; set; }
    public short[] SlotIds { get; set; }
    public short ChestSlot { get; set; }
}
