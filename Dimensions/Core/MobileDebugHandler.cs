using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrProtocol.Packets.Mobile;

namespace Dimensions.Core
{
    public class MobileDebugHandler : ClientHandler
    {
        public override void OnC2SPacket(PacketReceiveArgs args)
        {
            if (args.Packet is PlayerPlatformInfo packet)
            {
                Logger.Log("Client", LogLevel.DEBUG , $"收到PE数据包(平台: {packet.PlatformId},索引: {packet.PlayerId})");
            }
        }
    }
}
