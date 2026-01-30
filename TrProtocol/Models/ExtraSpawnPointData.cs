using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Models;

public partial struct ExtraSpawnPointData
{
    public byte Count { get; set; }
    public ShortPosition[] SpawnPoints { get; set; }
}
