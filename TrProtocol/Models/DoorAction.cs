namespace TrProtocol.Models;

[Serializer(typeof(PrimitiveFieldSerializer<DoorAction>))]
public enum DoorAction : byte
{
    OpenDoor = 0,
    CloseDoor,
    OpenTrapdoor,
    CloseTrapdoor,
    OpenTallGate,
    CloseTallGate
}
