using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrProtocol.Models;

public enum BannersMessageType : byte
{
    FullState,
    KillCountUpdate,
    ClaimCountUpdate,
    ClaimRequest,
    ClaimResponse
}
