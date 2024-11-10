using System.Reflection;
using System.Text;

namespace TrProtocol;

public abstract class Packet
{
    public abstract MessageID Type { get; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"[{GetType().Name}] ");
        sb.AppendJoin(", ",
            GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(prop =>
            {
                object value = prop.GetValue(this);
                if (prop.Name == nameof(Type)) return null;
                if (value is byte[] byteArray) return $"{prop.Name}=[{string.Join(", ", byteArray)}]";
                return $"{prop.Name}={value}";
            }));
        return sb.ToString();
    }
    
}

public interface IPlayerSlot
{
    byte PlayerSlot { get; set; }
}

public interface ILoadOutSlot
{
    byte LoadOutSlot { get; set; }
}

public interface IOtherPlayerSlot
{
    byte OtherPlayerSlot { get; set; }
}

public interface IItemSlot
{
    short ItemSlot { get; set; }
}

public interface IItemBase : IItemSlot
{
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    short Stack { get; set; }
    byte Prefix { get; set; }
    byte Owner { get; set; }
    short ItemType { get; set; }
}

public interface INPCSlot
{
    short NPCSlot { get; set; }
}

public interface IChestSlot
{
    short ChestSlot { get; set; }
}

public interface IProjSlot
{
    short ProjSlot { get; set; }
}

public abstract class NetModulesPacket : Packet
{
    public abstract NetModuleType ModuleType { get; }
}