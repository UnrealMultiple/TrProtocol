using TrProtocol;
using TrProtocol.Models;
using TrProtocol.Packets;
using TrProtocol.Packets.Modules;

namespace Dimensions.Core;

public class ConnectionHandler : ClientHandler
{
    public enum ClientState
    {
        New,
        ReusedConnect1,
        ReusedConnect2,
        Connected,
    }
    
    private ShortPosition spawnPosition;
    
    // once client received world data from server, set it to true and request tile data
    private ClientState state = ClientState.New;
    
    public override void OnS2CPacket(PacketReceiveArgs args)
    {
        if (args.Packet is WorldData data)
        {
            if (state != ClientState.ReusedConnect1) return;
            Parent.SendServer(new RequestTileData
            {
                Position = new Position(data.SpawnX, data.SpawnY)
            });
            spawnPosition = new ShortPosition(data.SpawnX, data.SpawnY);
            state = ClientState.ReusedConnect2;
        }
        else if (args.Packet is StartPlaying)
        {
            if (state != ClientState.ReusedConnect2) return;
            var spawn = new SpawnPlayer
            {
                PlayerSlot = Parent.syncPlayer.PlayerSlot,
                Context = PlayerSpawnContext.SpawningIntoWorld,
                DeathsPVE = 0,
                DeathsPVP = 0,
                Position = spawnPosition,
                Timer = 0
            };
            Parent.SendClient(spawn);
            Parent.SendServer(spawn);
            state = ClientState.Connected;
        }
        else if (args.Packet is LoadPlayer)
        {
            if (Parent.syncPlayer != null)
            {
                Parent.SendServer(Parent.syncPlayer);
            }
            
        }
    }

    public override void OnC2SPacket(PacketReceiveArgs args)
    {
        if (args.Packet is SyncPlayer sync)
        {
            if (Parent.syncPlayer != null && Parent.syncPlayer.Name != sync.Name)
            {
                Parent.Disconnect("禁止修改名字");
                args.Handled = true;
            }
            else
            {
                Parent.syncPlayer = sync;
                Parent.Name = sync.Name;
            }
        }
    }

    public override void OnCleaning()
    {
        state = ClientState.ReusedConnect1;
    }
}