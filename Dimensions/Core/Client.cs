﻿using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Dimensions.Models;
using Dimensions.Packets;
using TrProtocol;
using TrProtocol.Models;
using TrProtocol.Packets;
using TrProtocol.Packets.Modules;

namespace Dimensions.Core;

public class Client
{
    private readonly PacketClient _client;
    private ClientHello clientHello;
    public SyncPlayer syncPlayer;
    private readonly DimensionUpdate clientAddress;
    private Tunnel c2s, s2c;
    private PacketClient _serverConnection;
    private Server currentServer;
    private readonly List<ClientHandler> handlers = new ();
    public PacketClient PacketClient => _client;

    public string Name { get; set; }

    public void SendClient(Packet packet)
    {
        Logger.Log("Client", LogLevel.INFO , $"向客户端发送数据包: {packet}");
        _client.Send(packet);
    }

    public void SendChatMessage(string literal)
    {
        SendClient(new NetTextModuleS2C
        {
            Color = Color.White,
            PlayerSlot = 255,
            Text = NetworkText.FromLiteral(literal)
        });
    }
    
    public void SendServer(Packet packet)
    {
        //Console.WriteLine($"Send To Server: {packet}");
        _serverConnection!.Send(packet);
    }

    public Exception Disconnect(string reason)
    {
        SendClient(new Kick { Reason = NetworkText.FromLiteral(reason)});
        Logger.Log("Client", LogLevel.WARNING , $"已断开客户端{_client.client.Client?.RemoteEndPoint}的连接: {reason}");
        _client.client.Close();
        return new(reason);
    }
    
    public Client(TcpClient client)
    {
        _client = new PacketClient(client, true);

        _client.OnError += OnError;

        GlobalTracker.OnClientConnection(this);

        _client.Start();

        var packet = _client.Receive()!;

        if (packet is not ClientHello hello)
            throw new Exception("ClientHello expected!");
        
        clientHello = hello;

        var ip = client.Client.RemoteEndPoint as IPEndPoint;

        clientAddress = new DimensionUpdate
        {
            SubType = SubMessageID.ClientAddress,
            Content = ip.Address.ToString(),
            Port = (ushort)ip.Port
        };

        RegisterHandlers();
    }

    private void OnError(Exception e)
    {
        Logger.Log("Client", LogLevel.ERROR , $"连接错误: {e}");
        s2c?.Close();
        c2s?.Close();
        _serverConnection?.client.Close();
        _client?.client.Close();
    }

    public void TunnelTo(Server server)
    {
        _client.Clear();
        currentServer = server;
        var serverConnection = new TcpClient();
        serverConnection.Connect(server.serverIP!, server.serverPort);
        _serverConnection = new PacketClient(serverConnection, false);

        _serverConnection.OnError += OnError;

        _serverConnection.Start();

        // prepare the to-server channel to load player state
        _serverConnection.Send(clientHello);
        _serverConnection.Send(clientAddress);
        s2c = new Tunnel(_serverConnection, _client, "[S2C]");
        s2c.OnReceive += OnS2CPacket;
        s2c.OnError += OnError;
        
        c2s = new Tunnel(_client, _serverConnection, "[C2S]");
        c2s.OnReceive += OnC2SPacket;
        c2s.OnError += OnError;

        s2c.Start();
        c2s.Start();
    }

    private void OnCommonPacket(PacketReceiveArgs args)
    {
        foreach (var handler in handlers)
        {
            handler.OnCommonPacket(args);
            if (args.Handled) return;
        }
    }

    private void OnS2CPacket(PacketReceiveArgs args)
    {
        foreach (var handler in handlers)
        {
            handler.OnS2CPacket(args);
            if (args.Handled) return;
        }
        OnCommonPacket(args);
    }

    private void OnC2SPacket(PacketReceiveArgs args)
    {
        foreach (var handler in handlers)
        {
            handler.OnC2SPacket(args);
            if (args.Handled) return;
        }
        OnCommonPacket(args);
    }

    
    // clean existing entities
    private void Cleaning()
    {
        foreach (var handler in handlers)
        {
            handler.OnCleaning();
        }
    }

    public void ChangeServer(Server target)
    {
        if (target == null)
        {
            SendChatMessage("没有找到目标服务器");
            return;
        }
        
        if (target == currentServer)
        {
            SendChatMessage("你已连接此服务器");
            return;
        }
        
        try
        {
            Cleaning();
        }
        catch (Exception e)
        {
            Logger.Log("C2S", LogLevel.ERROR , $"在清理时发生错误: {e}");
        }
        
        s2c.OnReceive -= OnS2CPacket;
        c2s.OnReceive -= OnC2SPacket;
        _serverConnection.OnError -= OnError;
        
        s2c.Close();
        c2s.Close();
        
        _client.Cancel();
        _serverConnection!.Cancel();
        _serverConnection.client.Close();
        TunnelTo(target);

    }

    public void RegisterHandler<T>() where T : ClientHandler, new()
    {
        handlers.Add(new T().SetParent(this));
    }
    
    private void RegisterHandlers()
    {
        RegisterHandler<ConnectionHandler>();
        RegisterHandler<CommandHandler>();
        RegisterHandler<CustomPacketHandler>();
        RegisterHandler<NpcHandler>();
        RegisterHandler<ProjectileHandler>();
        RegisterHandler<PlayerHandler>();
        RegisterHandler<ItemHandler>();
        RegisterHandler<PylonHandler>();
        RegisterHandler<MobileDebugHandler>();
        RegisterHandler<SSCHandler>();
    }
}