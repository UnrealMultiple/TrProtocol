
using System.Net;
using System.Text.Json.Serialization;
using Dimensions.Core;
using Dimensions.Models;
using Newtonsoft.Json;

namespace Dimensions;

public static class Program
{
    public static Config Config;
    
    static Program()
    {
        Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"))!;
        Logger.Log("Config", LogLevel.INFO , $"协议版本号: {Config.protocolVersion}");
        Logger.Log("Config", LogLevel.INFO , $"侦听端口: {Config.listenPort}");
        Logger.Log("Config", LogLevel.INFO , $"远程服务器: {(Config.servers.Length == 0?"没有任何服务器配置捏~":string.Join(',',Config.servers.Select(x=> x.name)))}");
    }

    public static void Main(string[] args)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, Config.listenPort);
        var listener = new Listener(ipEndPoint);
        Logger.Log("TcpListener", LogLevel.INFO , $"正在侦听: {ipEndPoint.ToString()}");
        listener.ListenThread();
        
    }
}