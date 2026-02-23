namespace TrProtocol.Models;

[Serializer(typeof(PrimitiveFieldSerializer<LockAction>))]
public enum LockAction : byte
{
    UnlockChest = 1,
    UnlockDoor,
    LockChest
}
